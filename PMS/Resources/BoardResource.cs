using PMS.Models;
using PMS.Resources.SubResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class BoardResource
    {
        public int BoardId { get; set; }
        public string ResultGrade { get; set; }
        public string ResultScore { get; set; }
        public bool IsDeleted { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public string ProjectName { get; set; }
        public bool isAllScored { get; set; }
        public LecturerInformationResource LecturerInformations { get; set; }
        public ICollection<int> BoardEnrollments { get; set; }
        public BoardResource()
        {
            LecturerInformations = new LecturerInformationResource();
            BoardEnrollments = new Collection<int>();
            isAllScored = false;
            IsDeleted = false;
        }
    }
}
