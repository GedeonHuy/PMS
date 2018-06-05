using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class BoardEnrollment
    {
        public int BoardEnrollmentId { get; set; }
        public bool IsDeleted { get; set; }
        public double? Percentage { get; set; }
        public double? Score { get; set; }
        public bool isMarked { get; set; }
        public string Comment { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
        public Lecturer Lecturer { get; set; }
        public Board Board { get; set; }
        public BoardRole BoardRole { get; set; }
        public BoardEnrollment()
        {
            IsDeleted = false;
            isMarked = false;
            Recommendations = new Collection<Recommendation>();
        }
    }
}
