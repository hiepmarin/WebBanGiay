using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class taikhoan
    {

        public int acc_id { get; set; }
        
        [Required(ErrorMessage = "Bạn cần nhập Tên")]
        [Display(Name = "Tên")]
        public string name { get; set; }
        
        [Required(ErrorMessage = "Bạn cần nhập Tài khoản")]
        [Display(Name = "Tài khoản")]
        public string username { get; set; }
        
        [Required(ErrorMessage = "Bạn cần nhập Mật khẩu")]
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }
        
        [Required(ErrorMessage = "Bạn cần nhập SĐT")]
        [Display(Name = "Số điện thoại")]
        public string phone { get; set; }
        
        [Required(ErrorMessage = "Bạn cần nhập Email")]
        [Display(Name = "Email")]
        public string email { get; set; }
        
        [Required(ErrorMessage = "Bạn cần nhập Địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string address { get; set; }
        
        [Required(ErrorMessage = "Bạn cần nhập ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày sinh")]
        public DateTime birth { get; set; }
        public taikhoan()
        {

        }

    }
}