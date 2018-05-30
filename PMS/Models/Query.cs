using PMS.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Query : IQueryObject
    {
        public int? MajorId { get; set; }
        public int? ProjectId { get; set; }
        public int? LecturerId { get; set; }
        public int? StudentId { get; set; }
        public string CreatedDate { get; set; }
        public string isConfirm { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string TagName { get; set; }
        public string Name { get; set; }
        public int? QuarterId { get; set; }
        public string Email { get; set; }
        public string SortBy { get; set; }
        public bool isDeniedByLecturer { get; set; }
        public bool IsSortAscending { get; set; }
        public int? Page { get; set; }
        public byte PageSize { get; set; }

    }
}
