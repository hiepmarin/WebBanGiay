using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class order
    {
        public int acc_id { get; set; }
        public int bill_id { get; set; }
        public bool payment { get; set; }
        public int delivery_status { get; set; }
        public DateTime order_date { get; set; }
        public DateTime delivery_date { get; set; }
        public decimal total { get; set; }
        public string status { get; set; }
        public string action { get; set; }
    }
}