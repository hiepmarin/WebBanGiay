using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class product
    {
        public int shoe_id { get; set; }
        public string name { get; set; }
        public List<sss> sizeStockSold { get; set; }
        public int stock { get; set; }
        public int sold { get; set; }
        public string sex { get; set; }
        public string image { get; set; }
        public string category_id { get; set; }
        public string category_name { get; set; }
        public string detail { get; set; }
        public product()
        {
            sizeStockSold = new List<sss>();
            stock = 0;
            sold = 0;
        }
        public decimal price { get; set; }

        //public decimal getMinPrice(List<priceAndSize> priceAndSize)
        //{
        //    decimal min = new decimal();
        //    if (priceAndSize.Count > 1)
        //    {
        //        min = priceAndSize[0].price;
        //        for (int i = 0; i < priceAndSize.Count; i++)
        //        {
        //            if (priceAndSize[i].price < min)
        //            {
        //                min = priceAndSize[i].price;
        //            }
        //        }
        //    }
        //    return min;
        //}
        //public decimal getMaxPrice(List<priceAndSize> priceAndSize)
        //{
        //    decimal max = new decimal();
        //    if(priceAndSize.Count > 1)
        //    {
        //        max = priceAndSize[0].price;
        //        for (int i = 0; i < priceAndSize.Count; i++)
        //        {
        //            if (priceAndSize[i].price > max)
        //            {
        //                max = priceAndSize[i].price;
        //            }
        //        }
        //    }
        //    return max;
        //}
    }
    public class sss
    {
        public int size { get; set; }
        public int stock { get; set; }
        public int sold { get; set; }
    }

}