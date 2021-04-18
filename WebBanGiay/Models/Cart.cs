using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class Cart
    {
        MyDataDataContext data = new MyDataDataContext();
        public int sshoe_id { get; set; }
        public string nname { get; set; }
        public string iimage { get; set; }
        public string ssex { get; set; }
        public Double pprice { get; set; }
        public int qquantity { get; set; }
        public int ssize { get; set; }
        public Double amount
        {
            get { return pprice * qquantity; }
        }
        public Cart(int id, int size)
        {
            sshoe_id = id;
            ssize = size;
            shoe_info product = data.shoe_infos.Single(n => n.shoe_id == sshoe_id);
            nname = product.name;
            iimage = product.image;
            if(product.sex == 1)
            {
                ssex = "Nam";
            }
            else if(product.sex == 2)
            {
                ssex = "Nữ";
            }
            else
            {
                ssex = "Trẻ em";
            }
            shoe_info productt = data.shoe_infos.Single(a => a.shoe_id == sshoe_id);
            pprice = Convert.ToDouble(productt.price.ToString());
            qquantity = 1;
        }
    }
}