using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovingEstimator.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<Price> PriceEstimates { get; set; }
    }
}