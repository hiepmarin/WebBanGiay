using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiay.Models;

namespace WebBanGiay.Controllers
{
    public class ProductsController : Controller
    {
        List<product> list = new List<product>();
        productQuery query = new productQuery();
        List<category_table> categoryList = new List<category_table>();
        // GET: Products
        public ActionResult Index()
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;

            return View();
        }
        public ActionResult ShoesDetail(string shoe_id)
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;

            //List<priceAndSize> size = new List<priceAndSize>();
            //size = query.getPriceAndSize(shoe_id);
            //ViewBag.size = size;

            List<product> temp = query.get1Product(shoe_id);

            return View(temp);
        }
        public ActionResult Man()
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;

            list = query.getListProductForMan();
            return View(list);
        }
        public ActionResult Woman()
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;

            list = query.getListProductForWoman();
            return View(list);
        }
        public ActionResult Kid()
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;

            list = query.getListProductForKid();
            return View(list);
        }
        public ActionResult List(string sex)
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;

            if (sex == "1")
            {
                ViewBag.title = "Man";
            }
            else if (sex == "2")
            {
                ViewBag.title = "Woman";
            }
            else
            {
                ViewBag.title = "Kid";
            }

            list = query.getListProductForSex(sex);
            return View(list);
        }
        public ActionResult Shoes(string category_id)
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;

            list = query.getListProductCategory(category_id);
            if(category_id == "0")
            {
                ViewBag.category = "Khác";
            }
            else
            {
                ViewBag.category = query.getCategory_name(category_id);
            }
            return View(list);
        }
        public ActionResult All(string product)
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;

            if (product == "new")
            {
                list = query.getListProductType("new");
            }
            else
            {
                if (product == "sold")
                {
                    list = query.getListProductType("sold");
                }
                else
                {

                }
            }
            return View(list);
        }
        public ActionResult Order(string acc_id)
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;
            List<order> orderlist = query.getOrder(acc_id);
            return View(orderlist);
        }
        public ActionResult Order_Details(string bill_id, string acc_id)
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;
            List<orders_detail> list = query.getOrderDetail(bill_id);
            return View(list);
        }
        public ActionResult CancelOrder(string bill_id, string acc_id)
        {
            categoryList = query.getCategory();
            ViewBag.listCategory = categoryList;
            query.cancelOrder(bill_id);
            List<order> orderlist = query.getOrder(acc_id);
            return View("Order",orderlist);
        }
    }
}