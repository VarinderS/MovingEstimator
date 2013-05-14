using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MovingEstimator.Models
{
    public class Price
    {
        [Key]
        public int ID { get; set; }
        [JsonIgnore]
        public int LocationFromId { get; set; }
        [JsonIgnore]
        public int LocationToId { get; set; }

        public decimal OneBdrm { get; set; }
        public decimal ThreeBdrm { get; set; }
        public decimal FiveBdrm { get; set; }

        //[ForeignKey("LocationFromId")]
        public virtual Location From { get; set; }

        //[ForeignKey("LocationToId")]
        public virtual Location To { get; set; }
    }
}