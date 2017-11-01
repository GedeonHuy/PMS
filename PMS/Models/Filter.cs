using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Filter
    {
        public int? MajorId { get; set; }
        public int? ProjectId { get; set; }
        public int? LecturerId { get; set; }
        public bool? isConfirm { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public int? QuarterId { get; set; }
    }
}
