using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ClientCreateDto
    {

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name must contain only letters.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Addres { get; set; }
    }

    public class ClientUpdateDto : ClientCreateDto
    {
    }

    public class ClientUpdateDataDto
    {
        public string Addres { get; set; }
        public string PhoneNumber { get; set; }
    }


    public class ClientDto
    {
        public int Id { get; set; }
        public string IdentificationNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }


}
