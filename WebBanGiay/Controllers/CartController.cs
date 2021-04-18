using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiay.Models;

namespace WebBanGiay.Controllers
{
    public class CartController : Controller
    {
        DBConnection product = new DBConnection();
        MyDataDataContext data = new MyDataDataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public List<Cart> LayGioHang()
        {
            List<Cart> listGioHang = Session["Cart"] as List<Cart>;
            if (listGioHang == null)
            {
                //Neu gio hang chua ton tai thi khoi tao listGioHang
                listGioHang = new List<Cart>();
                Session["Cart"] = listGioHang;
            }
            return listGioHang;
        }

        public ActionResult ThemGioHang(int shoe_id, int size, string strURL)
        {
            //lay ra Session gio hang
            List<Cart> listGioHang = LayGioHang();
            //kiem tra sach nay ton tai trong session ["GioHang"] chua
            Cart sanpham = listGioHang.Find(n => n.sshoe_id == shoe_id && n.ssize == size);
            if (sanpham == null)
            {
                sanpham = new Cart(shoe_id, size);
                listGioHang.Add(sanpham);
                ViewBag.Tongsoluong = TongSoLuong();
                return Redirect(strURL);
            }
            else
            {
                sanpham.qquantity++;
                ViewBag.Tongsoluong = TongSoLuong();
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Cart> listGioHang = Session["Cart"] as List<Cart>;
            if (listGioHang != null)
            {
                iTongSoLuong = listGioHang.Sum(n => n.qquantity);
            }
            return iTongSoLuong;
        }

        private double TongTien()
        {
            double iTongTien = 0;
            List<Cart> listGioHang = Session["Cart"] as List<Cart>;
            if (listGioHang != null)
            {
                iTongTien = listGioHang.Sum(n => n.amount);
            }
            return iTongTien;
        }

        public ActionResult Cart()
        {
            List<Cart> listGioHang = LayGioHang();
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("HomePageForm", "HomePage");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(listGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        public ActionResult XoaGioHang(int shoe_id)
        {
            //lay gio hang tu session
            List<Cart> listGioHang = LayGioHang();
            //kiem tra sach da co trong session["GioHang"]
            Cart sanpham = listGioHang.SingleOrDefault(n => n.sshoe_id == shoe_id);
            //Neu ton tai thi cho sua soluong
            if (sanpham != null)
            {
                listGioHang.RemoveAll(n => n.sshoe_id == shoe_id);
                return RedirectToAction("Cart");
            }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("HomePageForm", "HomePage");
            }
            return RedirectToAction("Cart");
        }

        public ActionResult CapnhatGioHang(int shoe_id, int size, FormCollection f)
        {
            //lay gio hang tu session
            List<Cart> listGioHang = LayGioHang();
            //kiem tra sach da co trong session["GioHang"]
            Cart sanpham = listGioHang.SingleOrDefault(n => n.sshoe_id == shoe_id && n.ssize == size);
            //Neu ton tai thi cho sua soluong
            if (sanpham != null)
            {
                sanpham.qquantity = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Cart");
        }

        public ActionResult XoatatcaGioHang()
        {
            //Lay gio hang tu Session
            List<Cart> listGioHang = LayGioHang();
            listGioHang.Clear();
            return RedirectToAction("HomePageForm", "HomePage");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["user"] == null || Session["user"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("HomePageForm", "HomePage");
            }
            List<Cart> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return View(lstGioHang);
        }

        public ActionResult DatHang(FormCollection collection)
        {
            bill ddh = new bill();
            taikhoan kh = (taikhoan)Session["user"];
            List<Cart> gh = LayGioHang();
            ddh.acc_id = kh.acc_id;
            //ddh.payment = false;
            ddh.delivery_status = 0;
            ddh.order_date = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
            ddh.delivery_date = DateTime.Parse(ngaygiao);

            ddh.total = Convert.ToDecimal(TongTien().ToString());
            data.bills.InsertOnSubmit(ddh);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                order_detail ctdh = new order_detail();
                ctdh.bill_id = ddh.bill_id;
                ctdh.shoe_id = item.sshoe_id;
                ctdh.quantity = item.qquantity;
                //ctdh.price = (decimal)item.pprice;
                ctdh.size = item.ssize;
                data.order_details.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["Cart"] = null;
            return RedirectToAction("None", "Cart");
        }

        public ActionResult None()
        {
            return View();
        }

    }
}