using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Models
{
    public class Excel
    {
        public int ExcelId { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
    }
}
