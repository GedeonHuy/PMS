using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PMS.Data;
using PMS.Models;
using PMS.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class ExcelRepository : IExcelRepository
    {
        private ApplicationDbContext context;

        public ExcelRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Excel> GetExcel(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Excels.FindAsync(id);
            }
            return await context.Excels
                .SingleOrDefaultAsync(g => g.ExcelId == id);
        }

        public void AddExcel(Excel excel)
        {
            context.Excels.Add(excel);
        }

        public void RemoveExcel(Excel excel)
        {
            context.Excels.Remove(excel);
        }

        public async Task<IEnumerable<Excel>> GetExcels()
        {
            return await context.Excels
                    .ToListAsync();
        }

        public async Task AddStudent(Student student, ExcelWorksheet worksheet, int row)
        {
            student.StudentCode = worksheet.Cells[row, 2].Value.ToString();
            student.Name = worksheet.Cells[row, 3].Value.ToString().Trim() + " " +
                worksheet.Cells[row, 4].Value.ToString().Trim();
            student.Email = worksheet.Cells[row, 8].Value.ToString().Trim();
            student.Address = worksheet.Cells[row, 9].Value.ToString().Trim();
            student.PhoneNumber = worksheet.Cells[row, 10].Value.ToString().Trim();
            var majorName = worksheet.Cells[row, 11].Value.ToString().Trim().ToLower();
            var major = await context.Majors
                //.Include(m => m.Groups)
                ////.Include(m => m.Lecturers)
                //.Include(m => m.Students)
                //.Include(m => m.Projects)
                .FirstOrDefaultAsync(g => g.MajorName.ToLower() == majorName);
            student.Major = major;
        }

        public async Task AddLecturer(Lecturer lecturer, ExcelWorksheet worksheet, int row)
        {
            lecturer.Name = worksheet.Cells[row, 2].Value.ToString().Trim() + " " +
                worksheet.Cells[row, 3].Value.ToString().Trim();
            lecturer.DateOfBirth = DateTime.Parse(worksheet.Cells[row, 4].Value.ToString().Trim());
            lecturer.Address = worksheet.Cells[row, 5].Value.ToString().Trim();
            lecturer.Email = worksheet.Cells[row, 6].Value.ToString().Trim();
            lecturer.PhoneNumber = worksheet.Cells[row, 7].ToString().Trim();
            var majorName = worksheet.Cells[row, 8].Value.ToString().Trim().ToLower();
            var major = await context.Majors
                .Include(m => m.Groups)
                .Include(m => m.Lecturers)
                .Include(m => m.Students)
                .Include(m => m.Projects)
                .SingleOrDefaultAsync(g => g.MajorName.ToLower() == majorName);
            lecturer.Major = major;
        }

        public async Task AddProject(Project project, ExcelWorksheet worksheet, int row)
        {
            project.Title = worksheet.Cells[row, 2].Value.ToString().Trim() + " " +
                worksheet.Cells[row, 3].Value.ToString().Trim();
            project.ProjectCode = worksheet.Cells[row, 4].Value.ToString().Trim();
            project.Description = worksheet.Cells[row, 6].Value.ToString().Trim();
            project.Type = worksheet.Cells[row, 7].Value.ToString().Trim();

            var majorName = worksheet.Cells[row, 3].Value.ToString().Trim().ToLower();
            var major = await context.Majors
                .Include(m => m.Groups)
                .Include(m => m.Lecturers)
                .Include(m => m.Students)
                .Include(m => m.Projects)
                .SingleOrDefaultAsync(g => g.MajorName.ToLower() == majorName);
            project.Major = major;

            var lecturerName = worksheet.Cells[row, 5].Value.ToString().Trim().ToLower();
            var lecturer = await context.Lecturers
                .Include(l => l.Groups)
                .Include(l => l.CouncilEnrollments)
                .Include(p => p.Major)
                .SingleOrDefaultAsync(s => s.Name.ToLower() == lecturerName);
            project.Lecturer = lecturer;
        }
    }
}
