using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankServices.Models
{
    public class Client
    {
        public Guid ClientId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        public byte Level { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }
        public ICollection<Accounts> ClientAccounts { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
