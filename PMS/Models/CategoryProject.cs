using System;

namespace PMS.Models
{
    public class CategoryProject
    {
        public int CategoryProjectId { get; set; }
        public Category Category { get; set; }
        public Project Project { get; set; }
        public bool IsDeleted { get; set; }
        public CategoryProject()
        {
            IsDeleted = false;
        }
    }

}
