using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Models
{
    public class Accounts
    {
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public double Balance { get; set; }
        public DateTime OpenDate { get; set; }
        [Required]
        public Guid ClientId { get; set; }
    }
}
