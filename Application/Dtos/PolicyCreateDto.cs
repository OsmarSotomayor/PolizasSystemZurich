using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class PolicyCreateDto
    {
        [Required]
        [RegularExpression("Vida|Automóvil|Salud|Hogar", ErrorMessage = "Tipo inválido")]
        public string Type { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser positivo")]
        public decimal Amount { get; set; }

        [Required]
        [RegularExpression("Active|Cancelled", ErrorMessage = "Estado inválido")]
        public string State { get; set; } = "Active";

        [Required]
        public int ClientIdentificator { get; set; }
    }

    public class PolicyUpdateDto
    {
        public DateTime ExpirationDate { get; set; }
        public decimal Amount { get; set; }
        public string State { get; set; } // Active or Cancelled
    }

    public class PolicyResponseDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Amount { get; set; }
        public string State { get; set; }
        public int ClientIdentificator { get; set; }
    }
}
