using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PMS.Persistence;
using PMS.Resources;
using PMS.Models;
using PMS.Persistence.IRepository;
using PMS.Resources.SubResources;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using Aspose.Cells;
using System.Threading;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers
{
    [Route("/api/boards/")]
    public class BoardController : Controller
    {
        private IMapper mapper;
        private IBoardRepository boardRepository;
        private IGroupRepository groupRepository;
        private IBoardEnrollmentRepository boardEnrollmentRepository;
        private IHostingEnvironment host;
        private IExcelRepository excelRepository;
        private IConfiguration config;
        private IUnitOfWork unitOfWork;

        public BoardController(IMapper mapper, IUnitOfWork unitOfWork,
            IBoardRepository boardRepository, IGroupRepository groupRepository,
            IBoardEnrollmentRepository boardEnrollmentRepository, IHostingEnvironment host,
            IExcelRepository excelRepository, IConfiguration config)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.boardRepository = boardRepository;
            this.groupRepository = groupRepository;
            this.boardEnrollmentRepository = boardEnrollmentRepository;
            this.host = host;
            this.excelRepository = excelRepository;
            this.config = config;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateBoard([FromBody]BoardResource boardResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkLecturerInformations = boardRepository.CheckLecturerInformations(boardResource.LecturerInformations);

            //case: one percent of score is equal 0 or null          
            if (checkLecturerInformations == "nullScorePercent")
            {
                ModelState.AddModelError("Error", "One or more lecturer's percentage of score is not set");
                return BadRequest(ModelState);
            }

            //case: the total sum of score is not 100
            if (checkLecturerInformations == "sumScorePercentIsNot100")
            {
                ModelState.AddModelError("Error", "Total percentage of score is not equal 100%");
                return BadRequest(ModelState);
            }

            var board = mapper.Map<BoardResource, Board>(boardResource);
            var group = await groupRepository.GetGroup(boardResource.GroupId);
            board.Group = group;

            boardRepository.AddBoard(board);
            await unitOfWork.Complete();

            board = await boardRepository.GetBoard(board.BoardId);

            await boardRepository.AddLecturers(board, boardResource.LecturerInformations);
            await boardRepository.UpdateOrder(board, boardResource);

            await unitOfWork.Complete();

            if (board.BoardEnrollments.Count(c => c.isMarked == true) == board.BoardEnrollments.Count)
            {
                board.isAllScored = true;
                await unitOfWork.Complete();
            }

            var result = mapper.Map<Board, BoardResource>(board);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBoard(int id, [FromBody]BoardResource boardResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkLecturerInformations = boardRepository.CheckLecturerInformations(boardResource.LecturerInformations);

            //case: one percent of score is equal 0 or null          
            if (checkLecturerInformations == "nullScorePercent")
            {
                ModelState.AddModelError("Error", "One or more lecturer's percentage of score is not set");
                return BadRequest(ModelState);
            }

            //case: the total sum of score is not 100
            if (checkLecturerInformations == "sumScorePercentIsNot100")
            {
                ModelState.AddModelError("Error", "Total percentage of score is not equal 100%");
                return BadRequest(ModelState);
            }

            var board = await boardRepository.GetBoard(id, true);

            if (board == null)
                return NotFound();

            mapper.Map<BoardResource, Board>(boardResource, board);

            await boardRepository.UpdateBoardEnrollments(board, boardResource);

            await unitOfWork.Complete();

            var group = await groupRepository.GetGroup(boardResource.GroupId);
            board.Group = group;
            await unitOfWork.Complete();

            boardRepository.RemoveOldLecturer(board);
            await unitOfWork.Complete();

            // boardResource.ResultScore = calculateGrade(boardResource.LecturerInformations);

            await boardRepository.AddLecturers(board, boardResource.LecturerInformations);
            await unitOfWork.Complete();

            if (board.BoardEnrollments.Count(c => c.isMarked == true) == board.BoardEnrollments.Count)
            {
                board.isAllScored = true;
                await unitOfWork.Complete();
            }

            var result = mapper.Map<Board, BoardResource>(board);
            return Ok(result);
        }

        // private string calculateGrade(LecturerInformationResource lI)
        // {   
        //     double? pre = (lI.President.Score * 100) / lI.President.ScorePercent;
        //     double? sup = (lI.Supervisor.Score * 100) / lI.Supervisor.ScorePercent;
        //     double? sec = (lI.Secretary.Score * 100) / lI.Secretary.ScorePercent;
        //     double? rev = (lI.Reviewer.Score * 100) / lI.Reviewer.ScorePercent;

        //     return (pre + sup + sec + rev) + "";
        // }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            var board = await boardRepository.GetBoard(id, includeRelated: false);

            if (board == null)
            {
                return NotFound();
            }

            boardRepository.RemoveBoard(board);
            await unitOfWork.Complete();

            return Ok(id);
        }

        [HttpGet]
        [Route("getboard/{id}")]
        public async Task<IActionResult> GetBoard(int id)
        {
            var board = await boardRepository.GetBoard(id);

            if (board == null)
            {
                return NotFound();
            }

            //check number of Lecturer marked, and set isAllScored
            if (board.BoardEnrollments.Count(c => c.isMarked == true) == board.BoardEnrollments.Count)
            {
                board.isAllScored = true;
                await unitOfWork.Complete();
            }
            var boardResource = mapper.Map<Board, BoardResource>(board);

            if (boardResource.LecturerInformations == null)
            {
                boardResource.LecturerInformations = new LecturerInformationResource()
                {
                    Chair = new ChairResource()
                    {
                        ScorePercent = 25,
                        Score = 0
                    },
                    Secretary = new SecretaryResource()
                    {
                        ScorePercent = 25,
                        Score = 0
                    },
                    Supervisor = new SupervisorResource()
                    {
                        ScorePercent = 25,
                        Score = 0
                    },
                    Reviewer = new ReviewerResource()
                    {
                        ScorePercent = 25,
                        Score = 0
                    },
                };
            }

            return Ok(boardResource);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<QueryResultResource<BoardResource>> GetBoards(QueryResource queryResource)
        {
            var query = mapper.Map<QueryResource, Query>(queryResource);

            var queryResult = await boardRepository.GetBoards(query);
            return mapper.Map<QueryResult<Board>, QueryResultResource<BoardResource>>(queryResult);
        }

        [HttpGet]
        [Route("calculatescore/{id}")]
        public async Task<IActionResult> CalculateScore(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var board = await boardRepository.GetBoard(id);

            //check number of Lecturer marked, and set isAllScored
            if (board.BoardEnrollments.Count(c => c.isMarked == true) == board.BoardEnrollments.Count)
            {
                board.isAllScored = true;
                await unitOfWork.Complete();
            }
            if (board.isAllScored == false)
            {
                ModelState.AddModelError("Error", "One or a few lecturers have not marked yet");
                return BadRequest(ModelState);
            }

            board.ResultScore = boardRepository.CalculateScore(board).ToString();

            boardRepository.CalculateGrade(board);

            string excelFilePath = ExportExcelForStudents(board);
            string pdfFilePath = ConvertExcelToPdf(excelFilePath, board);
            SendMailForStudents(board, pdfFilePath);

            await unitOfWork.Complete();
            var result = mapper.Map<Board, BoardResource>(board);
            return Ok(result);
        }

        [HttpGet]
        [Route("sendformtolecturers/{id}")]
        public async Task<IActionResult> SendFormToLecturers(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var board = await boardRepository.GetBoard(id);
            foreach (var boardEnrollment in board.BoardEnrollments)
            {
                string excelFilePath = ExportExcelForLecturers(board, boardEnrollment);
                string pdfFilePath = ConvertExcelToPdf(excelFilePath, board);
                SendMailForLecturers(boardEnrollment, board, pdfFilePath);
            }

            await unitOfWork.Complete();
            var result = mapper.Map<Board, BoardResource>(board);
            return Ok(result);
        }

        public string ConvertExcelToPdf(string excelFilePath, Board board)
        {
            Aspose.Cells.License cellsLicense = new Aspose.Cells.License();

            cellsLicense.SetLicense("Aspose.Cells.lic");
            //open excel file
            Workbook workBook = new Workbook(excelFilePath);

            var fileName = board.Group.GroupName + "_" + board.BoardId + "_result" + @".pdf";
            var uploadFolderPath = Path.Combine(host.ContentRootPath, "exports/pdf");
            if (!System.IO.Directory.Exists(uploadFolderPath))
            {
                System.IO.Directory.CreateDirectory(uploadFolderPath);
            }

            var filePath = Path.Combine(uploadFolderPath, fileName);

            //save workbook in PDF format
            workBook.Save(filePath, SaveFormat.Pdf);
            return filePath;
        }

        public string ExportExcelForStudents(Board board)
        {

            var fileName = board.Group.GroupName + "_" + board.BoardId + "_resultForStudents" + @".xlsx";

            var formFolderPath = Path.Combine(host.ContentRootPath, "forms");
            if (!System.IO.Directory.Exists(formFolderPath))
            {
                System.IO.Directory.CreateDirectory(formFolderPath);
            }

            var formFilePath = Path.Combine(formFolderPath, @"result_form.xlsx");
            // FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            var uploadFolderPath = Path.Combine(host.ContentRootPath, "exports/excel");
            if (!System.IO.Directory.Exists(uploadFolderPath))
            {
                System.IO.Directory.CreateDirectory(uploadFolderPath);
            }

            var filePath = Path.Combine(uploadFolderPath, fileName);

            //copy file from formfolder to export folder
            System.IO.File.Copy(formFilePath, filePath, true);
            FileInfo file = new FileInfo(Path.Combine(uploadFolderPath, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int studentRows = board.Group.Enrollments.Count();
                worksheet.Cells[8, 3].Value = board.Group.Project.Title;

                int row = 12;
                var studentNames = board.Group.Enrollments.Select(e => e.Student.Name).ToList();
                foreach (var studentName in studentNames)
                {
                    worksheet.Cells[row, 3].Value = studentName;
                    row++;
                }

                row = 21;
                var lecturerInformations = board.BoardEnrollments.ToList();
                foreach (var lecturerInformation in lecturerInformations)
                {
                    worksheet.Cells[row, 2].Value = lecturerInformation.Lecturer.Name;
                    worksheet.Cells[row, 3].Value = lecturerInformation.BoardRole.BoardRoleName;
                    worksheet.Cells[row, 4].Value = lecturerInformation.Percentage;
                    worksheet.Cells[row, 5].Value = lecturerInformation.Comment;
                    worksheet.Cells[row, 6].Value = lecturerInformation.Score;
                    row++;
                }

                if (board.ResultScore != null)
                {
                    worksheet.Cells[25, 6].Value = board.ResultScore;
                    worksheet.Cells[26, 6].Value = board.ResultGrade;
                }

                worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.FitToWidth = 1;
                worksheet.PrinterSettings.FitToHeight = 0;
                package.Save();

                //add to db
                var excel = new Excel { FileName = fileName };
                excelRepository.AddExcel(excel);

                //send mail
                //SendMail(board, filePath);
                return filePath;
            }
        }

        public string ExportExcelForLecturers(Board board, BoardEnrollment boardEnrollment)
        {

            var fileName = board.Group.GroupName + "_" + board.BoardId + "_resultForLecturers" + @".xlsx";

            var formFolderPath = Path.Combine(host.ContentRootPath, "forms");
            if (!System.IO.Directory.Exists(formFolderPath))
            {
                System.IO.Directory.CreateDirectory(formFolderPath);
            }

            var formFilePath = Path.Combine(formFolderPath, @"lecturer_form.xlsx");
            // FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            var uploadFolderPath = Path.Combine(host.ContentRootPath, "exports/excel");
            if (!System.IO.Directory.Exists(uploadFolderPath))
            {
                System.IO.Directory.CreateDirectory(uploadFolderPath);
            }

            var filePath = Path.Combine(uploadFolderPath, fileName);

            //copy file from formfolder to export folder
            System.IO.File.Copy(formFilePath, filePath, true);
            FileInfo file = new FileInfo(Path.Combine(uploadFolderPath, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int studentRows = board.Group.Enrollments.Count();
                worksheet.Cells[8, 3].Value = board.Group.Project.Title;

                int row = 12;
                var studentNames = board.Group.Enrollments.Select(e => e.Student.Name).ToList();
                foreach (var studentName in studentNames)
                {
                    worksheet.Cells[row, 3].Value = studentName;
                    row++;
                }

                worksheet.Cells[16, 3].Value = boardEnrollment.Lecturer.Name;

                //add grade 
                var firstGrade = boardEnrollment.Grades.FirstOrDefault(g => g.GradeDescription.Equals("Trình bày tốt (Chuẩn bị slide tốt, trình bày rõ ràng đúng thời hạn)"));
                worksheet.Cells[23, 4].Value = firstGrade.Score;
                worksheet.Cells[23, 5].Value = firstGrade.Comment;

                var secondGrade = boardEnrollment.Grades.FirstOrDefault(g => g.GradeDescription.Equals("Nội dung đề tai đạt yêu cầu đặt ra, có tính khoa học"));
                worksheet.Cells[24, 4].Value = secondGrade.Score;
                worksheet.Cells[24, 5].Value = secondGrade.Comment;

                var thirdGrade = boardEnrollment.Grades.FirstOrDefault(g => g.GradeDescription.Equals("Phương pháp thực hiện tốt"));
                worksheet.Cells[25, 4].Value = thirdGrade.Score;
                worksheet.Cells[25, 5].Value = thirdGrade.Comment;

                var fourthGrade = boardEnrollment.Grades.FirstOrDefault(g => g.GradeDescription.Equals("Kết quả đề tài có áp dụng thực tế"));
                worksheet.Cells[26, 4].Value = fourthGrade.Score;
                worksheet.Cells[26, 5].Value = fourthGrade.Comment;

                var fifthGrade = boardEnrollment.Grades.FirstOrDefault(g => g.GradeDescription.Equals("Đề tài mới hoặc phương pháp thực hiện có tính sáng tạo"));
                worksheet.Cells[27, 4].Value = fifthGrade.Score;
                worksheet.Cells[27, 5].Value = fifthGrade.Comment;

                var sixthGrade = boardEnrollment.Grades.FirstOrDefault(g => g.GradeDescription.Equals("Trả lời tập trung vào đề tài, trả lời tốt câu hỏi"));
                worksheet.Cells[29, 4].Value = sixthGrade.Score;
                worksheet.Cells[29, 5].Value = sixthGrade.Comment;


                // worksheet.Cells[22, 2].Value = boardEnrollment.Score;
                // worksheet.Cells[22, 3].Value = boardEnrollment.Comment;
                // row = 22;
                // foreach (var recommendation in boardEnrollment.Recommendations)
                // {
                //     worksheet.Cells[row, 5].Value = recommendation.Description;
                //     row++;
                // }

                worksheet.Cells[32, 5].Value = "Ngày " + DateTime.Now.Day.ToString()
                + " Tháng " + DateTime.Now.Month.ToString() + " Năm " + DateTime.Now.Year.ToString();

                worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.FitToWidth = 1;
                worksheet.PrinterSettings.FitToHeight = 0;
                package.Save();

                //add to db
                var excel = new Excel { FileName = fileName };
                excelRepository.AddExcel(excel);

                //send mail
                //SendMail(board, filePath);
                return filePath;
            }
        }

        public async Task SendMailForStudents(Board board, string filePath)
        {
            var users = board.Group.Enrollments.Select(e => e.Student.Email).ToList();

            foreach (var user in users)
            {
                try
                {
                    string FromAddress = "quanhmp@gmail.com";
                    string FromAdressTitle = "Email from PMS!";
                    //To Address  
                    string ToAddress = user;
                    string ToAdressTitle = "PMS!";
                    string Subject = "Result of " + board.Group.Project.Type;
                    string BodyContent = "Xin gửi các em kết quả của báo cáo đồ án "
                    + board.Group.Project.Type + " của nhóm " + board.Group.GroupName;
                    //Smtp Server  
                    string SmtpServer = this.config["EmailSettings:Server"];
                    //Smtp Port Number  
                    int SmtpPortNumber = Int32.Parse(this.config["EmailSettings:Port"]);

                    var mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                    mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                    mimeMessage.Subject = Subject;

                    var builder = new BodyBuilder();
                    builder.TextBody = BodyContent;

                    // attach excel result file
                    builder.Attachments.Add(filePath);

                    // Now we just need to set the message body 
                    mimeMessage.Body = builder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {

                        client.Connect(SmtpServer, SmtpPortNumber, false);
                        // Note: only needed if the SMTP server requires authentication  
                        // Error 5.5.1 Authentication   
                        client.Authenticate(this.config["EmailSettings:Email"], this.config["EmailSettings:Password"]);
                        client.Send(mimeMessage);
                        client.Disconnect(true);

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task SendMailForLecturers(BoardEnrollment boardEnrollment, Board board, string filePath)
        {
            try
            {
                string FromAddress = "quanhmp@gmail.com";
                string FromAdressTitle = "Email from PMS!";
                //To Address  
                string ToAddress = boardEnrollment.Lecturer.Email;
                string ToAdressTitle = "PMS!";
                string Subject = "Result form of " + board.Group.Project.Type;
                string BodyContent = "Xin gửi các thầy form chấm điểm của báo cáo đồ án "
                + board.Group.Project.Type + " của nhóm " + board.Group.GroupName;
                //Smtp Server  
                string SmtpServer = this.config["EmailSettings:Server"];
                //Smtp Port Number  
                int SmtpPortNumber = Int32.Parse(this.config["EmailSettings:Port"]);

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                mimeMessage.Subject = Subject;

                var builder = new BodyBuilder();
                builder.TextBody = BodyContent;

                // attach excel result file
                builder.Attachments.Add(filePath);

                // Now we just need to set the message body 
                mimeMessage.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {

                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    // Note: only needed if the SMTP server requires authentication  
                    // Error 5.5.1 Authentication   
                    client.Authenticate(this.config["EmailSettings:Email"], this.config["EmailSettings:Password"]);
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}