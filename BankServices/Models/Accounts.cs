using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Models
{
    public class Accounts
    {
        public ObjectId _id { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public double Balance { get; set; }
        public DateTime OpenDate { get; set; }
        [Required]
        public ObjectId ClientId { get; set; }
    }
}
