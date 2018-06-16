using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PMS.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double Confidence { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<CategoryProject> CategoryProjects { get; set; }
        public Category()
        {
            IsDeleted = false;
            CategoryProjects = new Collection<CategoryProject>();
        }
    }

}
