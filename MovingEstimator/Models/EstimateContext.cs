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
            Configuration.ProxyCreationEnabled = false;
        }
        
    }
}