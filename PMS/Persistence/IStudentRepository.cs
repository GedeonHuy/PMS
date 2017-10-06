using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence
{
    public interface IStudentRepository
    {
        Task<Student> GetStudent(int id, bool includeRelated = true);
        void AddStudent(Student student);
        void RemoveStudent(Student student);
        Task<IEnumerable<Student>> GetStudents();
    }
}
