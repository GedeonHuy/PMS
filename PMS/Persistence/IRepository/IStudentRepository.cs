using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public interface IStudentRepository
    {
        Task<Student> GetStudent(int? id, bool includeRelated = true);
        Task<Student> GetStudentByEmail(string email);
        void AddStudent(Student student);
        void RemoveStudent(Student student);
        Task<IEnumerable<Student>> GetStudents();
        bool CheckStudentEnrollments(Student student, string Type);
    }
}
