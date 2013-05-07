using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovingEstimator.Models
{
    public class Price
    {
        public int PriceId { get; set; }
        public int LocationId { get; set; }
        public decimal PriceValue { get; set; }

        public virtual Location Location { get; set; }
    }
}