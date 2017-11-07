using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Project
    {   
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Enter alphabets and numbers only, please !")]
        [MaxLength(255, ErrorMessage = "Maximum length for the name is 255 characters.")]
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public Major Major { get; set; }
        public Lecturer Lecturer { get; set; }
        public ICollection<Group> Groups { get; set; }
        public Project()
        {
            Groups = new Collection<Group>();
        }
    }
}
