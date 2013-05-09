using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovingEstimator.Models
{
    public class EstimateContext : DbContext
    {
        public DbSet<Price> Prices { get; set; }
        public DbSet<Location> Locations { get; set; }

        public EstimateContext() 
        {
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Price>().HasRequired(p => p.From).WithMany(l => l.FromPrices).HasForeignKey(p => p.LocationFromId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Price>().HasRequired(p => p.To).WithMany(l => l.ToPrices).HasForeignKey(p => p.LocationToId).WillCascadeOnDelete(false);
        }
        
    }
}