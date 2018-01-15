using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeType { get; set; }
        public bool IsDeleted { get; set; }
        public Enrollment Enrollment { get; set; }
        public Grade()
        {
            IsDeleted = false;

        }
    }
}
