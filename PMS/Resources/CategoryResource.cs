using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PMS.Models;
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
        public ICollection<int> CategoryProjects { get; set; }
        public CategoryResource()
        {
            IsDeleted = false;
            CategoryProjects = new Collection<int>();
        }
    }

}
