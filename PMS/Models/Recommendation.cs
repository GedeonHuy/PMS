using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Recommendation
    {
        public int RecommendationId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDone { get; set; }
        public BoardEnrollment BoardEnrollment { get; set; }
        public Recommendation()
        {
            IsDone = false;
            IsDeleted = false;
        }
    }
}
