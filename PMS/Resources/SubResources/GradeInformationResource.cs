using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources.SubResources
{
    public class GradeInformationResource
    {
        public string GradeDescription { get; set; }
        public double? GradeMaxScore { get; set; }
        public double? Score { get; set; }
        public string Comment { get; set; }
    }
}
