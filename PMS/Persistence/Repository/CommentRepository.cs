using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using PMS.Models.TaskingModels;
using PMS.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private ApplicationDbContext context;

        public CommentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Comment> GetComment(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Comments.FindAsync(id);
            }
            return await context.Comments
                .Include(c => c.Task)
                .Include(c => c.User)
                .SingleOrDefaultAsync(g => g.CommentId == id);
        }

        public void AddComment(Comment Comment)
        {
            context.Comments.Add(Comment);
        }

        public void RemoveComment(Comment Comment)
        {
            Comment.IsDeleted = true;
            //context.Remove(Comment);
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await context.Comments
                    .Include(c => c.Task)
                    .Include(c => c.User)
                    .Where(c => c.IsDeleted == false)
                    .ToListAsync();
        }
    }
}
