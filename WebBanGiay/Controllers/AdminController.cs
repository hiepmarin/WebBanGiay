using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiay.Models;

namespace WebBanGiay.Controllers
{
    public class AdminController : Controller
    {
        //List<product> product;
        List<shoes_info_table> shoe_info;
        List<category_table> catList = new List<category_table>();
        adminQuery query = new adminQuery();

        // list category dùng để lưu các category lấy từ database
        List<category_table> ct = new List<category_table>();
        // list sexInfo dùng để lưu các sexInfo lấy từ database
        List<sexInfo> si = new List<sexInfo>();
        sexInfo temp = new sexInfo();

        // shoe_info_table để lưu thông tin từ table 'shoe_info'
        shoes_info_table shoe = new shoes_info_table();
        // 
        List<shoes_table> shoes_table_list = new List<shoes_table>();
        shoes_table shoes_table = new shoes_table();

        //

        // kiểm tra session user hiện tại có phải admin k
        public bool checkAdmin()
        {
            bool kq = false;
            if (Session["admin"] == null)
            {
                Session["user"] = null;
                Session["name"] = null;
                kq = false;
            }
            else
            {
                kq = true;
            }
            return kq;
        }

        // trang chủ của admin
        public ActionResult Index()
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        
        // trang đăng nhập của admin
        public ActionResult Login()
        {
            return View();
        }
        
        // hàm thực thi đăng nhập
        [HttpPost, ActionName("Login")]
        public ActionResult Login(taikhoan acc)
        {
            taikhoanQuery query = new taikhoanQuery();
            if (query.AdminTryLogin(acc.username, acc.password))
            {
                taikhoan a = query.getAdminInfo(acc.username);
                Session["user"] = a;
                Session["name"] = a.name;
                Session["admin"] = true;
                ViewBag.message = "Login succesfully!";
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.message = "Login failed!";
            return View();
        }

        // đăng xuất
        public ActionResult LogOff()
        {
            Session["user"] = null;
            Session["admin"] = null;
            return RedirectToAction("Index", "Admin");
        }

        // return view Shoes hiện tất cả giày
        public ActionResult Shoes()
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            shoe_info = query.getShoe_info();
            return View(shoe_info);
        }

        // return view Add Shoe_info
        public ActionResult AddShoes()
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }

            ct = query.getListCategory();
            shoe.categorylist = ct;

            si = query.getListSex();
            shoe.sexList = si;

            return View(shoe);
        }

        // add data to Shoe_info
        [HttpPost, ActionName("AddShoes")]
        [ValidateAntiForgeryToken]
        public ActionResult AddShoes(shoes_info_table shoes_info, HttpPostedFileBase file)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                shoes_info.image = "/Content/img/" + fileName;
                file.SaveAs((path));
            }
            if (query.addShoe_info(shoes_info))
            {
                shoe_info = query.getShoe_info();
                return View("Shoes", shoe_info);
            }
            return View("Shoes", shoes_info);
        }

        // return view edit shoe_info
        public ActionResult EditShoe_info(string shoe_id)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            shoe = query.get1Shoes(shoe_id);

            ct = query.getListCategory();
            shoe.categorylist = ct;

            si = query.getListSex();
            shoe.sexList = si;

            return View(shoe);
        }

        // edit shoe in shoe_info table
        [HttpPost, ActionName("EditShoe_info")]
        [ValidateAntiForgeryToken]
        public ActionResult EditShoe_info(shoes_info_table shoes_info, HttpPostedFileBase file)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                shoes_info.image = "/Content/img/" + fileName;
                file.SaveAs((path));
            }
            if (query.editShoe_info(shoes_info))
            {
                shoe_info = query.getShoe_info();
                return View("Shoes", shoe_info);
            }
            return View("EditShoe_info");
        }

        // delete shoes in shoe_info table
        public ActionResult DeleteShoe_info(string shoe_id)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            if (query.deleteShoe_info(shoe_id))
            {
                shoe_info = query.getShoe_info();
                return View("Shoes", shoe_info);
            }
            return View("Shoes", shoe_id);
        }

        // return view in shoes table
        public ActionResult Details(string shoe_id)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            shoes_table_list = query.getShoes_table(shoe_id);
            if(shoes_table_list.Count == 0)
            {
                ViewBag.message = "Sản phẩm này không có thông tin";
            }
            else
            {
                ViewBag.message = null;
            }
            Session["shoe_id"] = shoe_id;
            return View(shoes_table_list);
        }

        // return view add 'Shoes' table
        public ActionResult AddShoesDetails(string shoe_id)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.id = shoe_id;
            return View();
        }

        // thêm data vào bảng Shoes
        [HttpPost, ActionName("AddShoesDetails")]
        [ValidateAntiForgeryToken]
        public ActionResult AddShoesDetails(shoes_table shoe)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            query.addShoes(shoe);
            return RedirectToAction("Details", "Admin", new { shoe_id = shoe.shoe_id});
        }

        // return view edit 'Shoes' table
        public ActionResult EditShoesDetails(string shoe_id, string size)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            shoes_table = query.get1Shoes_table(shoe_id, size);
            shoes_table.info.shoe_id = Convert.ToInt32(shoe_id);
            shoes_table.info.size = Convert.ToInt32(size);
            Session["shoe_id"] = Convert.ToInt32(shoe_id);
            Session["size"] = Convert.ToInt32(size);

            return View(shoes_table);
        }

        // sửa data bảng shoes
        [HttpPost, ActionName("EditShoesDetails")]
        [ValidateAntiForgeryToken]
        public ActionResult EditShoesDetails(shoes_table shoe)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            if (query.editShoes(shoe, Session["shoe_id"].ToString(), Session["size"].ToString(), Session["color"].ToString()))
            {
                
                return RedirectToAction("Details", "Admin", new { shoe_id = Session["shoe_id"]});
            }
            //return RedirectToAction("EditShoesDetails", "Admin", new { shoe_id = shoe.shoe_id, size = shoe.size, color = shoe.color });
            return View("EditShoesDetails");
        }
        
        // xóa data bảng shoes
        public ActionResult Delete_Shoes_Details(string shoe_id, string size, string color)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            query.deleteShoes(shoe_id, size, color);
            return RedirectToAction("Details", "Admin", new { shoe_id = shoe_id });
        }

        // xem danh sách category
        public ActionResult Categories()
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            List<category_table> cat = query.getListCategory();
            return View(cat);
        }
        
        // return view thêm category
        public ActionResult AddCategory()
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        // thêm category
        [HttpPost, ActionName("AddCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(category_table cat)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            query.addCategory(cat);
            catList = query.getListCategory();
            return View("Categories", catList);
        }

        // return view sửa category
        public ActionResult EditCategory(string category_id)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            category_table cat = query.getCategory(category_id);
            return View(cat);
        }

        // sửa category
        [HttpPost, ActionName("EditCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(category_table category)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            query.editCategory(category);
            catList = query.getListCategory();
            return View("Categories", catList);
        }

        // xóa category
        public ActionResult DeleteCategory(string category_id)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            if (query.deleteCategory(category_id))
            {
                //
            }

            catList = query.getListCategory();
            return View("Categories", catList);
        }

        // return view bill hiện tất cả bill
        public ActionResult Order()
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            List<order> listOrder = new List<order>();
            listOrder = query.getBill();
            return View(listOrder);
        }

        // thực thi action với bill
        public ActionResult EditOrder(string bill_id)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            query.changeDelivery_status(bill_id);
            List<order> listOrder = new List<order>();
            listOrder = query.getBill();
            return View("Order",listOrder);
        }
        // hủy đơn
        public ActionResult CancelOrder(string bill_id)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            query.cancelOrder(bill_id);
            List<order> listOrder = new List<order>();
            listOrder = query.getBill();
            return View("Order", listOrder);
        }

        // view orderdetails
        public ActionResult OrderDetails(string bill_id)
        {
            if (!checkAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            List<orders_detail> list = query.getOrderDetail(bill_id);
            return View(list);
        }

    }
}