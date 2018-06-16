using System;
using PMS.Resources;

namespace PMS.Models
{
    public class CategoryProjectResource
    {
        public int CategoryProjectId { get; set; }
        public CategoryResource Category { get; set; }
        public int? CategoryId { get; set; }
        public ProjectResource Project { get; set; }
        public int? ProjectId { get; set; }
        public bool IsDeleted { get; set; }
        public CategoryProjectResource()
        {
            IsDeleted = false;
        }
    }

}