namespace PMS.Resources
{
    public class TagProjectResource
    {
        public int TagProjectId { get; set; }
        public bool IsDeleted { get; set; }
        public int? TagId { get; set; }
        public TagResource Tag { get; set; }
        public int? ProjectId { get; set; }
        public ProjectResource Project { get; set; }
        public TagProjectResource()
        {
            IsDeleted = false;
        }
    }
}