using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Resources
{
    public class QueryResource
    {
        //filter
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
        public string StudentCode { get; set; }
        public int? QuarterId { get; set; }
        public string Email { get; set; }
        public string SortBy { get; set; }
        public bool isDeniedByLecturer { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
        public string BoardRoleName { get; set; }
        public string ResultScore { get; set; }
        public bool IsNotAssigned { get; set; }

        //search
        public string StudentCodeSearch { get; set; }
        public string NameSearch { get; set; }
        public string AddressSearch { get; set; }
        public string EmailSearch { get; set; }
        public string PhoneNumberSearch { get; set; }
        public string ProjectCodeSearch { get; set; }
        public string TitleSearch { get; set; }
        public string DescriptionSearch { get; set; }
        public string GroupNameSearch { get; set; }
        public string LinkGitHubSearch { get; set; }
        public string ResultGradeSearch { get; set; }
        public string ResultScoreSearch { get; set; }

    }
}
