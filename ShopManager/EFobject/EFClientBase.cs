using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Access.Dao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.EFClient
{
    internal class EFClientBase : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Client> Clients {get;set;}
        
        public EFClientBase()
        {
            Database.EnsureCreated();
         
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ConnectProducts"].ConnectionString);
        }
    }
}
