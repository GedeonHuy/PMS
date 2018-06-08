using System;

namespace PMS.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double Confidence { get; set; }
        public bool IsDeleted { get; set; }
        public Project Project { get; set; }
        public Category()
        {
            IsDeleted = false;
        }
    }

}
