using System;
namespace PMS.Models.TaskingModels
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public ApplicationUser User { get; set; }
        public Task Task { get; set; }
        public Comment()
        {
            IsDeleted = false;
        }
    }
}