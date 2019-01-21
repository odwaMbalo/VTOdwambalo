using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Models
{
    public class ClientContext : DbContext
    { 
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        { 
          
        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<ClientProfile> ClientProfile { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
               .HasKey(c => new { c.ClientId });
            modelBuilder.Entity<Accounts>()
                .HasKey(c => new { c.AccountNumber});
        }
    }
}
