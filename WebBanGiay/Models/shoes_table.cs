using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class shoes_table
    {
        public int shoe_id { get; set; }
        [Required]
        public int size { get; set; }
        [Required]
        public int stock { get; set; }
        [Required]
        public int sold { get; set; }
        public shoes_table_edit info {get;set;}
        public shoes_table()
        {
            info = new shoes_table_edit();
        }
    }
    public class shoes_table_edit
    {
        public int shoe_id { get; set; }
        public int size { get; set; }
        public string color { get; set; }
    }
}