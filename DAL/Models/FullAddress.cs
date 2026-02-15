using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class FullAddress
    {
        public String City { get; set; } = null!;
        public String Street { get; set; } = null!;
        public Int32 HouseNumber { get; set; }
        public String PostalCode { get; set; } = null!;

    }
}
