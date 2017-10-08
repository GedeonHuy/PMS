using System.ComponentModel.DataAnnotations;

namespace PMS.Resources
{
    public class RoleResource
    {    
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}