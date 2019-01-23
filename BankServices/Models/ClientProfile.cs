using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Models
{
    public class ClientProfile
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
