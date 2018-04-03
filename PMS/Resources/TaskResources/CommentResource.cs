namespace PMS.Resources.TaskResources
{
    public class CommentResource
    {
        public int CommentId { get; set; }
        public int TaskId { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public ApplicationUserResource User { get; set; }
        public TaskResource Task { get; set; }
        public CommentResource()
        {
            IsDeleted = false;
        }
    }
}