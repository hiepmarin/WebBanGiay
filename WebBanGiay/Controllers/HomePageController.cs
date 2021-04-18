using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiay.Models;

namespace WebBanGiay.Controllers
{
    public class HomePageController : Controller
    {
        List<product> list = new List<product>();
        productQuery query = new productQuery();
        List<category_table> categoryList = new List<category_table>();
        // GET: HomePage
        public ActionResult HomePageForm()
        {
            //bool kq = Convert.ToBoolean(Session["admin"].ToString());
            //if (kq)
            //{
            //    Session["user"] = null;
            //    Session["username"] = null;
            //}
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;

            list = query.getListProductForHomePage();
            //list = query.getListProduct("Giày");

            

            return View(list);
        }
        public ActionResult Search(string name)
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;

            list = query.getListProduct(name);
            return View(list);
        }

    }
}