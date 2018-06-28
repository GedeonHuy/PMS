using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeDescription { get; set; }
        public double? GradeMaxScore { get; set; }
        public string Comment { get; set; }
        public double? Score { get; set; }
        public bool IsDeleted { get; set; }
        public BoardEnrollment BoardEnrollment { get; set; }
        public Grade()
        {
            IsDeleted = false;

        }
    }
}
