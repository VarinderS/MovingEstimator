﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MovingEstimator.Models
{
    public class Location
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Price> FromPrices { get; set; }

        [JsonIgnore]
        public virtual ICollection<Price> ToPrices { get; set; }

    }
}