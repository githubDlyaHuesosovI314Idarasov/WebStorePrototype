using Redis.OM.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Location : Entity
    {
        public FullAddress Address { get; set; } = null!;
        // public GeoLoc Coordinates { get; set; }
        public IEnumerable<Stock> Stocks { get; set; } = new List<Stock>();


    }
}
