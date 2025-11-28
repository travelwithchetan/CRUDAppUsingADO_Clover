using System.ComponentModel.DataAnnotations;

namespace CRUDAppUsingADO.Models
{
    public class Employees
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public string designation { get; set; }
        [Required]
        public string city { get; set; }

    }
}
