using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MovingEstimator.Models;

namespace MovingEstimator.App_Start
{
    public class DbInitializer : DropCreateDatabaseAlways<EstimateContext>
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
                new Price { LocationFromId = 1, LocationToId = 2, OneBdrm = 100, ThreeBdrm = 100, FiveBdrm = 100 },
                new Price { LocationFromId = 2, LocationToId = 3, OneBdrm = 200, ThreeBdrm = 200, FiveBdrm = 200 },
                new Price { LocationFromId = 3, LocationToId = 4, OneBdrm = 300, ThreeBdrm = 300, FiveBdrm = 300 },
                new Price { LocationFromId = 4, LocationToId = 1, OneBdrm = 400, ThreeBdrm = 400, FiveBdrm = 400 }
            };

            price.ForEach(p => context.Prices.Add(p));
            context.SaveChanges();
        }
    }
}