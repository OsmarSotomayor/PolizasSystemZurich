using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Policy
    {
        public Guid Id { get; set; }
        public string Type { get; set; } // Life, Auto, etc.
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Amount { get; set; }
        public string State { get; set; } // Active, Cancelled
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
