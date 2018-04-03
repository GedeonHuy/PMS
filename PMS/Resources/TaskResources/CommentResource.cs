namespace PMS.Resources.TaskResources
{
    public class CommentResource
    {
        public int CommentResourceId { get; set; }
        public int TaskId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public ApplicationUserResource User { get; set; }
        public TaskResource Task { get; set; }
        public CommentResource()
        {
            IsDeleted = false;
        }
    }
}