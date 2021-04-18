using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class orders_detail
    {
        public int bill_id { get; set; }
        public int shoe_id { get; set; }
        public int size { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}