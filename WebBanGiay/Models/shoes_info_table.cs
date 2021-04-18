using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class shoes_info_table
    {
        [Required]
        public int shoe_id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string image { get; set; }
        [Required]
        public string category { get; set; }
        public string categoryName { get; set; }
        public IEnumerable<category_table> categorylist { get; set; }
        [Required]
        public string detail { get; set; }
        [Required]
        public byte sex { get; set; }
        public string sexName { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        public IEnumerable<sexInfo> sexList { get; set; }
        public shoes_info_table()
        {
            
        }
    }
    public class sexInfo
    {
        public byte sexId { get; set; }
        public string sexName { get; set; }
    }
    
}