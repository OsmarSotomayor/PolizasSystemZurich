using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Client
    {
        [Key]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Identification number must be numeric and 10 digits long.")]
        public string IdentificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public string? Address { get; set; }
        public ICollection<Policy> Policies { get; set; }
    }
}
