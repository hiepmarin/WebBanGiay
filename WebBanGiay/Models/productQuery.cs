using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class productQuery
    {
        private DBConnection db;

        public productQuery()
        {
            db = new DBConnection();
        }

        // Lấy 1 product theo shoe_id 
        // Dùng cho shoe detail
        public product getProduct(string shoe_id)
        {
            product temp = new product();

            string sql = "select a.shoe_id, b.name, b.image, b.detail, a.size, c.category_name, a.stock, a.sold, b.sex from shoes a, shoe_info b, category c " +
                "where a.shoe_id = b.shoe_id and b.category_id = c.category_id and a.shoe_id = '"+shoe_id+"' group by a.shoe_id, b.name, b.image, b.detail, a.size, c.category_name, a.stock, a.sold, b.sex";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            if (dt.Rows.Count != 0)
            {
                temp.shoe_id = Convert.ToInt32(dt.Rows[0]["shoe_id"].ToString());
                temp.name = dt.Rows[0]["name"].ToString();
                temp.image = dt.Rows[0]["image"].ToString();
                temp.detail = dt.Rows[0]["detail"].ToString();
                temp.category_name = dt.Rows[0]["category_name"].ToString();
                int sex = Convert.ToInt32(dt.Rows[0]["sex"].ToString());
                if(sex == 1)
                {
                    temp.sex = "Nam";
                }
                else if(sex == 2)
                {
                    temp.sex = "Nữ";
                }
                else
                {
                    temp.sex = "Trẻ em";
                }
                //List<int> size = new List<int>();
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    size.Add(Convert.ToInt32(dt.Rows[i]["size"].ToString()));
                //}
                //temp.size = size;
            }

            return temp;
        }

        // Lấy 1 product theo shoe_id 
        // Dùng cho shoe detail
        public List<product> get1Product(string shoe_id)
        {
            List<product> list = new List<product>();
            product temp = new product();

            string sql = "select a.shoe_id, b.name, b.image, b.detail, a.size, c.category_name, a.stock, a.sold, b.sex, b.price " +
                "from shoes a, shoe_info b, category c " +
                "where a.shoe_id = b.shoe_id and b.category_id = c.category_id and a.stock != 0 and a.shoe_id = '" + shoe_id + "' " +
                "group by a.shoe_id, b.name, b.image, b.detail, a.size, c.category_name, a.stock, a.sold, b.sex, b.price";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            if (dt.Rows.Count != 0)
            {
                // lấy các thông tin cơ bản
                temp.shoe_id = Convert.ToInt32(dt.Rows[0]["shoe_id"].ToString());
                temp.name = dt.Rows[0]["name"].ToString();
                temp.image = dt.Rows[0]["image"].ToString();
                temp.detail = dt.Rows[0]["detail"].ToString();
                temp.category_name = dt.Rows[0]["category_name"].ToString();
                int sex = Convert.ToInt32(dt.Rows[0]["sex"].ToString());
                if (sex == 1)
                {
                    temp.sex = "Nam";
                }
                else if (sex == 2)
                {
                    temp.sex = "Nữ";
                }
                else
                {
                    temp.sex = "Trẻ em";
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sss dungtam = new sss();
                    dungtam.stock = Convert.ToInt32(dt.Rows[i]["stock"].ToString());
                    dungtam.sold = Convert.ToInt32(dt.Rows[i]["sold"].ToString());
                    dungtam.size = (Convert.ToInt32(dt.Rows[i]["size"].ToString()));
                    temp.sizeStockSold.Add(dungtam);
                    temp.stock += dungtam.stock;
                    temp.sold += dungtam.sold;
                }
                // thêm product vừa lấy được vào List<product>
                // list chỉ gồm 1 product
                list.Add(temp);
            }

            return list;
        }

        // lấy danh sách category_id va category_name
        public List<category_table> getCategory()
        {
            string sql = "select category_name, category_id from category";

            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            //SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            //DataTable dt = new DataTable();
            List<category_table> temp = new List<category_table>();
            while (reader.Read())
            {
                category_table cat = new category_table();
                cat.category_idd = reader.GetInt32(1).ToString();
                cat.category_namee = reader.GetString(0);
                temp.Add(cat);
            }
            cmd.Dispose();
            con.Close();
            return temp;
        }
        
        // lấy danh sách giày cho home page (select order by sold)
        public List<product> getListProductForHomePage()
        {
            List<product> list = new List<product>();

            //string sql = "select top 10 with ties a.shoe_id, b.name, b.image, b.detail, a.sold, c.category_name, b.sex from shoes a, shoe_info b, category c " +
            //    "where a.shoe_id = b.shoe_id and b.category_id = c.category_id group by a.shoe_id, b.name, b.image, b.detail, a.sold, c.category_name, b.sex order by a.sold";
            string sql = "select a.shoe_id, b.name, b.image, b.detail, b.sex, c.category_name, sum(a.sold) as sold, b.price from shoes a, shoe_info b, category c " +
                "where a.shoe_id = b.shoe_id and b.category_id = c.category_id  group by a.shoe_id, b.name, b.image, b.detail, b.sex, c.category_name, b.price order by sum(a.sold) desc";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();
            
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                product temp = new product();
                temp.shoe_id = Convert.ToInt32(dt.Rows[i]["shoe_id"].ToString());
                temp.name = dt.Rows[i]["name"].ToString();
                temp.image = dt.Rows[i]["image"].ToString();
                temp.detail = dt.Rows[i]["detail"].ToString();
                temp.category_name = dt.Rows[i]["category_name"].ToString();
                temp.price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
                int sex = Convert.ToInt32(dt.Rows[i]["sex"].ToString());
                if (sex == 1)
                {
                    temp.sex = "Nam";
                }
                else if (sex == 2)
                {
                    temp.sex = "Nữ";
                }
                else
                {
                    temp.sex = "Trẻ em";
                }

                list.Add(temp);
            }

            return list;
        }
        
        // lấy danh sách giày truyền vào tên
        // dùng cho tìm kiếm
        public List<product> getListProduct(string name)
        {
            List<product> list = new List<product>();
            string sql = "";
            if(name == "")
            {

            }
            else
            {
                sql = "select top 20 with ties a.shoe_id, b.name, b.image, b.detail, a.sold from shoes a, shoe_info b " +
                "where a.shoe_id = b.shoe_id and b.name like N'%" + name + "%'  group by a.shoe_id, b.name, b.image, b.detail, a.sold order by a.sold";
            }
            
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                product temp = new product();
                temp.shoe_id = Convert.ToInt32(dt.Rows[i]["shoe_id"].ToString());
                temp.name = dt.Rows[i]["name"].ToString();
                temp.image = dt.Rows[i]["image"].ToString();
                temp.detail = dt.Rows[i]["detail"].ToString();
                list.Add(temp);
            }

            return list;
        }

        // lấy danh sách giày theo giới tính
        public List<product> getListProductForSex(string sex)
        {
            List<product> list = new List<product>();

            string sql = "select a.shoe_id, b.name, b.image, b.detail, c.category_name, b.price from shoes a, shoe_info b, category c " +
                "where a.shoe_id = b.shoe_id and b.category_id = c.category_id and b.sex = '"+sex+"' group by a.shoe_id, b.name, b.image, b.detail, c.category_name, b.price";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                product temp = new product();
                temp.shoe_id = Convert.ToInt32(dt.Rows[i]["shoe_id"].ToString());
                temp.name = dt.Rows[i]["name"].ToString();
                temp.image = dt.Rows[i]["image"].ToString();
                temp.detail = dt.Rows[i]["detail"].ToString();
                temp.category_name = dt.Rows[i]["category_name"].ToString();
                temp.price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
                list.Add(temp);
            }
            return list;
        }

        // lấy danh sách giày cho nam
        public List<product> getListProductForMan()
        {
            List<product> list = new List<product>();

            string sql = "select a.shoe_id, b.name, b.image, b.detail, c.category_name, b.price from shoes a, shoe_info b, category c " +
                "where a.shoe_id = b.shoe_id and b.category_id = c.category_id and b.sex = 1 group by a.shoe_id, b.name, b.image, b.detail, c.category_name, b.price";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                product temp = new product();
                temp.shoe_id = Convert.ToInt32(dt.Rows[i]["shoe_id"].ToString());
                temp.name = dt.Rows[i]["name"].ToString();
                temp.image = dt.Rows[i]["image"].ToString();
                temp.detail = dt.Rows[i]["detail"].ToString();
                temp.category_name = dt.Rows[i]["category_name"].ToString();
                temp.price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
                list.Add(temp);
            }
            return list;
        }
        
        // lấy danh sách giày cho nữ
        public List<product> getListProductForWoman()
        {
            List<product> list = new List<product>();

            string sql = "select a.shoe_id, b.name, b.image, b.detail, c.category_name, b.price from shoes a, shoe_info b, category c " +
                "where a.shoe_id = b.shoe_id and b.category_id = c.category_id and b.sex = 2 group by a.shoe_id, b.name, b.image, b.detail, c.category_name, b.price";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                product temp = new product();
                temp.shoe_id = Convert.ToInt32(dt.Rows[i]["shoe_id"].ToString());
                temp.name = dt.Rows[i]["name"].ToString();
                temp.image = dt.Rows[i]["image"].ToString();
                temp.detail = dt.Rows[i]["detail"].ToString();
                temp.category_name = dt.Rows[i]["category_name"].ToString();
                temp.price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
                list.Add(temp);
            }
            return list;
        }

        // lấy danh sách giày cho trẻ em
        public List<product> getListProductForKid()
        {
            List<product> list = new List<product>();

            string sql = "select a.shoe_id, b.name, b.image, b.detail, c.category_name, b.price from shoes a, shoe_info b, category c " +
                "where a.shoe_id = b.shoe_id and b.category_id = c.category_id and b.sex = 3 group by a.shoe_id, b.name, b.image, b.detail, c.category_name, b.price";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                product temp = new product();
                temp.shoe_id = Convert.ToInt32(dt.Rows[i]["shoe_id"].ToString());
                temp.name = dt.Rows[i]["name"].ToString();
                temp.image = dt.Rows[i]["image"].ToString();
                temp.detail = dt.Rows[i]["detail"].ToString();
                temp.category_name = dt.Rows[i]["category_name"].ToString();
                temp.price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
                list.Add(temp);
            }
            return list;
        }

        // lấy danh sách giày theo category
        public List<product> getListProductCategory(string category_id)
        {
            List<product> list = new List<product>();
            string sql = "";
            if ("0" == category_id)
            {
                sql = "select a.shoe_id, b.name, b.image, b.detail, a.sold, c.category_name, b.sex from shoes a, shoe_info b, category c " +
                "where a.shoe_id = b.shoe_id and b.category_id = c.category_id and c.category_id = NULL group by a.shoe_id, b.name, b.image, b.detail, a.sold, c.category_name, b.sex order by a.sold";
            }
            else
            {
                sql = "select a.shoe_id, b.name, b.image, b.detail, b.sex, SUM(a.sold) as sold " +
                    "from shoes a, shoe_info b, category c " +
                    "where a.shoe_id = b.shoe_id and b.category_id = c.category_id and c.category_id = '"+category_id+"' " +
                    "group by a.shoe_id, b.name, b.image, b.detail, b.sex " +
                    "order by SUM(a.sold) desc";
            }
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                product temp = new product();
                temp.shoe_id = Convert.ToInt32(dt.Rows[i]["shoe_id"].ToString());
                temp.name = dt.Rows[i]["name"].ToString();
                temp.image = dt.Rows[i]["image"].ToString();
                temp.detail = dt.Rows[i]["detail"].ToString();
                //temp.category_name = dt.Rows[i]["category_name"].ToString();
                int sex = Convert.ToInt32(dt.Rows[i]["sex"].ToString());
                if (sex == 1)
                {
                    temp.sex = "Nam";
                }
                else if (sex == 2)
                {
                    temp.sex = "Nữ";
                }
                else
                {
                    temp.sex = "Trẻ em";
                }
                list.Add(temp);
            }
            return list;
        }
        
        // lấy category_name từ categry_id
        public string getCategory_name(string category_id)
        {
            string sql = "select category_name from category where category_id = '" + category_id+ "'";
            
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            //SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            //DataTable dt = new DataTable();
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            cmd.Dispose();
            con.Close();
            return null;
        }

        // lấy product theo sản phẩm bán chạy, sản phẩm mới
        public List<product> getListProductType(string type)
        {
            List<product> list = new List<product>();
            string sql = "";
            if(type == "new")
            {
                sql = "select a.shoe_id, b.name, b.image, b.detail, c.category_name, b.date from shoes a, shoe_info b, category c " +
                "where a.shoe_id = b.shoe_id and b.category_id = c.category_id group by a.shoe_id, b.name, b.image, b.detail, c.category_name, b.date order by b.date desc";
            }
            if (type == "sold")
            {
                sql = "select a.shoe_id, b.name, b.image, b.detail, c.category_name, sum(a.sold) as sold  " +
                    "from shoes a, shoe_info b, category c " +
                    "where a.shoe_id = b.shoe_id and b.category_id = c.category_id  " +
                    "group by a.shoe_id, b.name, b.image, b.detail, c.category_name " +
                    "order by sum(a.sold) desc";
            }
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                product temp = new product();
                temp.shoe_id = Convert.ToInt32(dt.Rows[i]["shoe_id"].ToString());
                temp.name = dt.Rows[i]["name"].ToString();
                temp.image = dt.Rows[i]["image"].ToString();
                temp.detail = dt.Rows[i]["detail"].ToString();
                temp.category_name = dt.Rows[i]["category_name"].ToString();
                list.Add(temp);
            }
            return list;
        }

        // lấy danh sách order của user
        public List<order> getOrder(string acc_id)
        {
            string sql = "select * from bill where acc_id = '"+acc_id+"'";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            List<order> list = new List<order>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                order temp = new order();
                temp.bill_id = Convert.ToInt32(dt.Rows[i]["bill_id"].ToString());
                temp.acc_id = Convert.ToInt32(dt.Rows[i]["acc_id"].ToString());
                //temp.payment = Convert.ToBoolean(dt.Rows[i]["payment"].ToString());
                temp.delivery_status = Convert.ToInt32(dt.Rows[i]["delivery_status"].ToString());
                temp.order_date = Convert.ToDateTime(dt.Rows[i]["order_date"].ToString());
                temp.delivery_date = Convert.ToDateTime(dt.Rows[i]["delivery_date"].ToString());
                temp.total = Convert.ToDecimal(dt.Rows[i]["total"].ToString());
                temp.status = getStatus(temp.delivery_status);
                temp.action = getAction(temp.delivery_status);
                list.Add(temp);
            }
            return list;
        }

        // lấy string status từ int delivery_status
        public string getStatus(int delivery_status)
        {
            if (delivery_status == 0)
            {
                return "Đang chờ xác nhận";
            }
            else if (delivery_status == 1)
            {
                return "Đã xác nhận, đang đóng gói";
            }
            else if (delivery_status == 2)
            {
                return "Đã đóng gói, đang vận chuyển";
            }
            else if (delivery_status == 3)
            {
                return "Đã giao hàng";
            }
            else if (delivery_status == -1)
            {
                return "Đã hủy";
            }
            if (delivery_status == -2)
            {
                return "Đã hủy bởi admin";
            }
            return "";
        }
        
        // lấy acction đối với 1 order từ delivery_status
        public string getAction(int delivery_status)
        {
            if (delivery_status == 0)
            {
                return "Xác nhận";
            }
            else if (delivery_status == 1)
            {
                return "Đang vận chuyển";
            }
            else if (delivery_status == 2)
            {
                return "Đã giao hàng";
            }
            else if (delivery_status == 3)
            {
                return "None";
            }
            else if (delivery_status == -1)
            {
                return "None";
            }
            if (delivery_status == -2)
            {
                return "None";
            }

            return "None";
        }



    }
}