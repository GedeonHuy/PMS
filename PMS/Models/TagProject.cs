using System;
namespace PMS.Models
{
    public class TagProject
    {
        public int TagProjectId { get; set; }
        public bool IsDeleted { get; set; }
        public Tag Tag { get; set; }
        public Project Project { get; set; }
        public TagProject()
        {
            IsDeleted = false;
        }
    }
}