using PMS.Models;
using PMS.Resources;
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

        Task<Student> GetStudentByStudentCode(string studentCode);
        
        void AddStudent(Student student);
        void RemoveStudent(Student student);
        void UpdateEnrollments(Student student, StudentResource studentResource);
        Task<QueryResult<Student>> GetStudents(Query filter);
        bool CheckStudentEnrollments(Student student, string Type);
        Task<QueryResult<Enrollment>> GetEnrollments(Query queryObj, string email);
        Task<QueryResult<Group>> GetGroups(Query queryObj, string email);
    }
}
