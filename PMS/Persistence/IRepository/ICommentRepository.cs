using System.Collections.Generic;
using System.Threading.Tasks;
using PMS.Models.TaskingModels;

namespace PMS.Persistence.IRepository
{
    public interface ICommentRepository
    {
        Task<Comment> GetComment(int? id, bool includeRelated = true);
        void AddComment(Comment comment);
        void RemoveComment(Comment comment);
        Task<IEnumerable<Comment>> GetComments();
    }
}