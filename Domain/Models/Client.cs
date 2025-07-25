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
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "El numero de identificacion solo puede tener 10 digitos")]
        public int IdentificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public ICollection<Policy> Policies { get; set; }
    }
}
