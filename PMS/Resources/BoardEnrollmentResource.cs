using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class BoardEnrollmentResource
    {
        public int BoardEnrollmentId { get; set; }
        public bool IsDeleted { get; set; }
        public int? LecturerID { get; set; }
        public int? BoardID { get; set; }
        public int Percentage { get; set; }
        public double? Score { get; set; }
        public bool isMarked { get; set; }
        public string Comment { get; set; }
        public ICollection<String> Recommendations { get; set; }
        public BoardResource Board { get; set; }
        public LecturerResource Lecturer { get; set; }
        public BoardEnrollmentResource()
        {
            Lecturer = new LecturerResource();
            Board = new BoardResource();
            IsDeleted = false;
            Recommendations = new Collection<String>();
        }
    }
}
