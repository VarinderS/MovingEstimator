using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MovingEstimator.Models;

namespace MovingEstimator.App_Start
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<EstimateContext>
    {
        protected override void Seed(EstimateContext context)
        {
            var loc = new List<Location>
            {
                new Location { Name = "Auckland" },
                new Location { Name = "Warkworth" },
                new Location { Name = "Snells Beach" },
                new Location { Name = "Welsford" }
            };

            loc.ForEach(l => context.Locations.Add(l));
            context.SaveChanges();

            var price = new List<Price>
            {
                new Price { LocationFromId = 1, LocationToId = 2, PriceValue = 100 },
                new Price { LocationFromId = 2, LocationToId = 3, PriceValue = 200 },
                new Price { LocationFromId = 3, LocationToId = 4, PriceValue = 300 },
                new Price { LocationFromId = 4, LocationToId = 1, PriceValue = 400 }
            };

            price.ForEach(p => context.Prices.Add(p));
            context.SaveChanges();
        }
    }
}