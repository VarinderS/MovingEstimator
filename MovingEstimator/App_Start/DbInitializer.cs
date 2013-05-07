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
                new Location { LocationName = "Auckland" },
                new Location { LocationName = "Warkworth" },
                new Location { LocationName = "Snells Beach" },
                new Location { LocationName = "Welsford" }
            };

            loc.ForEach(l => context.Locations.Add(l));
            context.SaveChanges();

            var price = new List<Price>
            {
                new Price { LocationId = 1, PriceValue = 100 },
                new Price { LocationId = 2, PriceValue = 200 },
                new Price { LocationId = 3, PriceValue = 300 },
                new Price { LocationId = 4, PriceValue = 400 }
            };

            price.ForEach(p => context.Prices.Add(p));
            context.SaveChanges();
        }
    }
}