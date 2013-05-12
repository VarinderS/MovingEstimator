using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MovingEstimator.Models
{
    public class PriceDto
    {
        public PriceDto() { }
        public PriceDto(Price price)
        {
            this.ID = price.ID;
            this.LocationFromId = price.From.ID;
            this.LocationFrom = price.From.Name;
            this.LocationToId = price.To.ID;
            this.LocationTo = price.To.Name;
            this.PriceValue = price.PriceValue;
        }

        [Key]
        public int ID { get; set; }
        
        public int LocationFromId { get; set; }

        public string LocationFrom { get; set; }
        
        public int LocationToId { get; set; }

        public string LocationTo { get; set; }

        public decimal PriceValue { get; set; }

        public Price ToEntity()
        {
            return new Price
            {
                ID = this.ID,
                LocationFromId = this.LocationFromId,
                LocationToId = this.LocationToId,
                PriceValue = this.PriceValue
            };
        }
    }
}