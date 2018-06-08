using System;
using PMS.Resources;

namespace PMS.Resources
{
    public class CategoryResource
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double Confidence { get; set; }
        public bool IsDeleted { get; set; }
        public int? ProjectId { get; set; }
        public ProjectResource Project { get; set; }
        public CategoryResource()
        {
            IsDeleted = false;
        }
    }

}
