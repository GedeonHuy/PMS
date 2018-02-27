using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Board
    {
        public int BoardId { get; set; }
        public string ResultGrade { get; set; }
        public string ResultScore { get; set; }
        public bool IsDeleted { get; set; }
        public bool isAllScored { get; set; }
        public Group Group { get; set; }
        public ICollection<BoardEnrollment> BoardEnrollments { get; set; }
        public Board()
        {
            BoardEnrollments = new Collection<BoardEnrollment>();
            isAllScored = false;
            IsDeleted = false;
        }
    }

}
