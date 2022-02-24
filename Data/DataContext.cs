using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SBTBackEnd.Entities;
using SBTBackEnd.Entities.Product;
using SBTBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SBTBackEnd.Data
{
    public class DataContext : IdentityDbContext<SBTUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubProduct> SubProducts { get; set; }
        public DbSet<FeedbackMessage> FeedbackMessages { get; set; }
        
        public DbSet<User> SBTUsers { get; set; }
		public DbSet<SBTClient> SBTClients  { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			
		}
        
    }
}
