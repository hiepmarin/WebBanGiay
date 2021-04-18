using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiay.Models;

namespace WebBanGiay.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost, ActionName("SignUp")]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(taikhoan acc)
        {
            taikhoanQuery temp = new taikhoanQuery();
            if (temp.CreateAccount(acc))
            {
                Session["message"] = "You has sign up succesfully!";
                return RedirectToAction("Login", "Account");
            }
            ViewBag.fail = "Username đã tồn tại ";
            //Session["message"] = "Failed!";
            return View("Signup", acc);
        }
        
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost, ActionName("Login")]
        public ActionResult Login(taikhoan acc)
        {
            taikhoanQuery query = new taikhoanQuery();
            if(query.TryLogin(acc.username, acc.password))
            {
                taikhoan a = query.getInfo(acc.username);
                Session["user"] = a;
                Session["name"] = a.name;
                Session["username"] = a.username;
                Session["acc_id"] = a.acc_id;
                //Session["message"] = "Đăng nhập thành công";
                ViewBag.message = "Login succesfully!";
                return RedirectToAction("HomePageForm", "HomePage");
            }
            Session["message"] = "Login failed!";
            ViewBag.message = "Login failed!";
            return View();
        }
        
        public ActionResult Manage(string username)
        {
            taikhoan temp = new taikhoan();
            taikhoanQuery query = new taikhoanQuery();
            temp = query.getInfo(username);
            //return View();
            return RedirectToAction("ManageAccount", "Account", temp);
        }
        
        public ActionResult ManageAccount(taikhoan acc)
        {
            taikhoanQuery query = new taikhoanQuery();
            //acc = query.getInfo();
            return View(acc);
        }

        public ActionResult EditAccount(string username)
        {
            taikhoan temp = new taikhoan();
            taikhoanQuery query = new taikhoanQuery();
            temp = query.getInfo(username);
            return View(temp);
        }

        [HttpPost, ActionName("EditAccount")]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount(taikhoan acc)
        {
            taikhoanQuery query = new taikhoanQuery();
            query.EditAccount(acc);
            acc = query.getInfo(acc.username);
            return RedirectToAction("ManageAccount", "Account", acc);
        }

        public ActionResult LogOff()
        {
            Session["user"] = null;
            Session["username"] = null;
            return RedirectToAction("HomePageForm", "HomePage");
        }
    }
}