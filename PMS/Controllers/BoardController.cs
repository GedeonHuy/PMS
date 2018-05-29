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
        private IUnitOfWork unitOfWork;

        public BoardController(IMapper mapper, IUnitOfWork unitOfWork,
            IBoardRepository boardRepository, IGroupRepository groupRepository,
            IBoardEnrollmentRepository boardEnrollmentRepository, IHostingEnvironment host)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.boardRepository = boardRepository;
            this.groupRepository = groupRepository;
            this.boardEnrollmentRepository = boardEnrollmentRepository;
            this.host = host;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateBoard([FromBody]BoardResource boardResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var checkLecturerInformations = boardRepository.CheckLecturerInformations(boardResource.LecturerInformations);

            // //case: one percent of score is equal 0 or null          
            // if (checkLecturerInformations == "nullOrZeroScorePercent")
            // {
            //     ModelState.AddModelError("Error", "One or more lecturer's percentage of score is 0 or null");
            //     return BadRequest(ModelState);
            // }

            // //case: the total sum of score is not 100
            // if (checkLecturerInformations == "sumScorePercentIsNot100")
            // {
            //     ModelState.AddModelError("Error", "If total percentage of score is not equal 100%");
            //     return BadRequest(ModelState);
            // }

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
            if (checkLecturerInformations == "nullOrScorePercent")
            {
                ModelState.AddModelError("Error", "One or more lecturer's percentage of score is 0 or null");
                return BadRequest(ModelState);
            }

            //case: the total sum of score is not 100
            if (checkLecturerInformations == "sumScorePercentIsNot100")
            {
                ModelState.AddModelError("Error", "If total percentage of score is not equal 100%");
                return BadRequest(ModelState);
            }

            var board = await boardRepository.GetBoard(id);

            if (board == null)
                return NotFound();

            mapper.Map<BoardResource, Board>(boardResource, board);

            boardRepository.UpdateBoardEnrollments(board, boardResource);

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
            await unitOfWork.Complete();

            var result = mapper.Map<Board, BoardResource>(board);
            return Ok(result);
        }

        [HttpGet]
        [Route("calculategrade/{id}")]
        public async Task<IActionResult> CalculateGrade(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var board = await boardRepository.GetBoard(id);

            if (String.IsNullOrEmpty(board.ResultScore))
            {
                ModelState.AddModelError("Error", "Please calcualte the score before calcualate the grade.");
                return BadRequest(ModelState);
            }

            boardRepository.CalculateGrade(board);
            await unitOfWork.Complete();

            var result = mapper.Map<Board, BoardResource>(board);
            return Ok(result);
        }

        [HttpGet]
        [Route("exportexcel/{id}")]
        public async Task ExportCustomer(int id)
        {
            var board = await boardRepository.GetBoard(id);
            var fileName = board.Group.GroupName + "_result" + @".xlsx";

            string rootFolder = host.WebRootPath;
            var formFolderPath = Path.Combine(host.WebRootPath, "forms");
            var formFilePath = Path.Combine(formFolderPath, @"result_form.xlsx");
            // FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            var uploadFolderPath = Path.Combine(host.WebRootPath, "exports");
            var filePath = Path.Combine(uploadFolderPath, fileName);

            if (!System.IO.Directory.Exists(uploadFolderPath))
            {
                System.IO.Directory.CreateDirectory(uploadFolderPath);
            }

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
                    worksheet.Cells[row, 4].Value = lecturerInformation.Comment;
                    worksheet.Cells[row, 5].Value = lecturerInformation.Score;
                    row++;
                }

                if (board.ResultScore != null)
                {
                    worksheet.Cells[25, 5].Value = board.ResultScore;
                }
                package.Save();
            }
        }
    }
}
