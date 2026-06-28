using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class DeliveryNotify
    {
        public String Title { get; set; } = null!;
        public String ThumbnailUrl { get; set; } = null!;
        public String WaybillNumber { get; set; } = null!;
        public String CourierCompany { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public OrderStatus Status { get; set; }
        public String Text { get; set; } = null!;
    }
}
