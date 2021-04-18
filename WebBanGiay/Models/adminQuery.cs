using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class adminQuery
    {
        DBConnection db = new DBConnection();
        
        /*
         ------------------------truy xuất dữ liệu bảng Shoe_info trong database------------------------
        */
        // lấy thông tin 1 đôi giày
        public shoes_info_table get1Shoes(string shoe_id)
        {
            shoes_info_table temp = new shoes_info_table();
            string sql = "select * from shoe_info where shoe_id = '" + shoe_id + "'";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                temp.name = reader.GetString(1);
                temp.image = reader.GetString(2);
                temp.detail = reader.GetString(3);
                temp.category = reader.GetInt32(4).ToString();
                temp.sex = Convert.ToByte(reader.GetInt32(5));
                temp.date = reader.GetDateTime(6);
                temp.price = reader.GetDecimal(7);
            }
            reader.Close();
            con.Close();
            return temp;
        }

        // lấy danh sách tất cả giày
        public List<shoes_info_table> getShoe_info()
        {
            List<shoes_info_table> list = new List<shoes_info_table>();

            string sql = "select * from shoe_info";
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
                shoes_info_table temp = new shoes_info_table();
                temp.shoe_id = Convert.ToInt32(dt.Rows[i]["shoe_id"].ToString());
                temp.name = dt.Rows[i]["name"].ToString();
                temp.image = dt.Rows[i]["image"].ToString();
                temp.detail = dt.Rows[i]["detail"].ToString();
                temp.price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
                //temp.category = Convert.ToInt32(dt.Rows[i]["category_id"].ToString());
                temp.category = dt.Rows[i]["category_id"].ToString();
                temp.date = Convert.ToDateTime(dt.Rows[i]["date"].ToString());
                int sex = Convert.ToByte(dt.Rows[i]["sex"].ToString());
                if (sex == 1)
                {
                    temp.sexName = "Nam";
                }
                else if (sex == 2)
                {
                    temp.sexName = "Nữ";
                }
                else
                {
                    temp.sexName = "Trẻ em";
                }
                temp.categoryName = getCategoryName(temp.category);
                list.Add(temp);
            }

            return list;
        }

        // thêm data vào bảng shoe_info
        public bool addShoe_info(shoes_info_table shoe)
        {
            string sql = "insert into shoe_info(name, image, detail, category_id, sex, date) " +
                "values('" + shoe.name + "','" + shoe.image + "','" + shoe.detail + "','" + shoe.category + "', '" + shoe.sex + "', '"+DateTime.Now+"')";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kq = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return kq > 0;
        }

        // sửa data bảng shoe_info 
        public bool editShoe_info(shoes_info_table shoe)
        {
            string sql = "update shoe_info set name = '" + shoe.name + "', image = '" + shoe.image + "', detail = '" + shoe.detail + "', " +
                "category_id = '" + shoe.category + "', sex =  '" + shoe.sex + "' where shoe_id = '" + shoe.shoe_id + "'";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kq = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return kq > 0;
        }

        // xóa data bảng shoe_info
        public bool deleteShoe_info(string shoe_id)
        {
            string sql = "delete from shoe_info where shoe_id = '" + shoe_id + "'";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kq = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return kq > 0;
        }

        /*
         ------------------------truy xuất dữ liệu bảng Category trong database------------------------
        */
        // lấy danh sách category (id, name)
        public List<category_table> getListCategory()
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

        // lấy category_name từ category_id
        public string getCategoryName(string category_id)
        {
            string sql = "select category_name from category where category_id = '" + category_id + "'";

            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            string temp = "";
            if (reader.Read())
            {
                temp = reader.GetString(0);
            }
            cmd.Dispose();
            con.Close();
            return temp;
        }

        // lấy 1 category theo id
        public category_table getCategory(string category_id)
        {
            string sql = "select category_name, category_id from category where category_id = '"+category_id+"'";

            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            category_table cat = new category_table();
            if (reader.Read())
            {
                cat.category_idd = reader.GetInt32(1).ToString();
                cat.category_namee = reader.GetString(0);
            }
            cmd.Dispose();
            con.Close();
            return cat;
        }

        // thêm category
        public bool addCategory(category_table cat)
        {
            string sql = "insert into category(category_name) values('"+cat.category_namee+"')";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kq = cmd.ExecuteNonQuery();
            return kq > 0;
        }

        // sửa category
        public bool editCategory(category_table cat)
        {
            string sql = "update category set category_name = '" + cat.category_namee + "' where category_id = '" + cat.category_idd + "'";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kq = cmd.ExecuteNonQuery();
            return kq > 0;
        }

        // xóa category
        public bool deleteCategory(string category_id)
        {
            string sql = "delete from category where category_id = '"+category_id+"'";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kq = cmd.ExecuteNonQuery();
            return kq > 0;
        }

        /*
         ------------------------truy xuất dữ liệu bảng Shoes trong database------------------------
        */
        // lấy data bảng shoes
        public List<shoes_table> getShoes_table(string shoe_id)
        {
            string sql = "select * from shoes where shoe_id = '" + shoe_id + "'";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            List<shoes_table> list = new List<shoes_table>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                shoes_table temp = new shoes_table();
                temp.shoe_id = Convert.ToInt32(dt.Rows[i]["shoe_id"].ToString());
                temp.size = Convert.ToInt32(dt.Rows[i]["size"].ToString());
                temp.stock = Convert.ToInt32(dt.Rows[i]["stock"].ToString());
                temp.sold = Convert.ToInt32(dt.Rows[i]["sold"].ToString());
                list.Add(temp);
            }
            return list;
        }

        // lấy 1 cột trong shoes table
        public shoes_table get1Shoes_table(string shoe_id, string size)
        {
            shoes_table temp = new shoes_table();
            string sql = "select * from shoes where shoe_id = '"+shoe_id+ "' and size = '"+size+ "'";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                temp.shoe_id = reader.GetInt32(0);
                temp.size = reader.GetInt32(1);
                temp.stock = (int)reader.GetInt64(2);
                temp.sold = (int)reader.GetInt64(3);
            }
            return temp;
        }

        // lấy danh sách sex(id,name)
        public List<sexInfo> getListSex()
        {
            List<sexInfo> list = new List<sexInfo>();
            sexInfo temp1 = new sexInfo();
            sexInfo temp2 = new sexInfo();
            sexInfo temp3 = new sexInfo();
            temp1.sexId = 1;
            temp1.sexName = "Nam";
            list.Add(temp1);
            temp2.sexId = 2;
            temp2.sexName = "Nữ";
            list.Add(temp2);
            temp3.sexId = 3;
            temp3.sexName = "Trẻ em";
            list.Add(temp3);
            return list;

        }

        // thêm data bảng shoes
        public bool addShoes(shoes_table shoe)
        {
            string sql = "insert into shoes(shoe_id, size, color, price, stock, sold) " +
                "values('" + shoe.shoe_id + "','" + shoe.size + "','" + shoe.stock + "', 0)";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kq = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return kq > 0;
        }

        // sửa data bảng shoes
        public bool editShoes(shoes_table shoe, string shoe_id, string size, string color)
        {
            string sql = "update shoes set size = '"+shoe.size+ "'," +
                "stock = '"+shoe.stock+ "', sold = '"+shoe.sold+ "' " +
                "where shoe_id = '"+shoe_id+ "' and size = '"+size+ "' and color = '"+color+"'";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kq = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return kq > 0;
        }

        // xóa data bảng shoes
        public bool deleteShoes(string shoe_id, string size, string color)
        {
            string sql = "delete from shoes where shoe_id = '" + shoe_id + "' and size = '"+size+"'";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kq = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return kq > 0;
        }

        /*
         ------------------------truy xuất dữ liệu bảng bill trong database------------------------
        */
        // lấy dữ liệu bảng bill
        public List<order> getBill()
        {
            string sql = "select * from bill";
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
                temp.payment = Convert.ToBoolean(dt.Rows[i]["payment"].ToString());
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

        // lấy 1 bill
        public order getABill(string bill_id)
        {
            string sql = "select * from bill where bill_id = '" + bill_id + "'";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            // open con
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            order temp = new order();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                temp.bill_id = Convert.ToInt32(dt.Rows[i]["bill_id"].ToString());
                temp.acc_id = Convert.ToInt32(dt.Rows[i]["acc_id"].ToString());
                temp.payment = Convert.ToBoolean(dt.Rows[i]["payment"].ToString());
                temp.delivery_status = Convert.ToInt32(dt.Rows[i]["delivery_status"].ToString());
                temp.order_date = Convert.ToDateTime(dt.Rows[i]["order_date"].ToString());
                temp.delivery_date = Convert.ToDateTime(dt.Rows[i]["delivery_date"].ToString());
                temp.total = Convert.ToDecimal(dt.Rows[i]["total"].ToString());
                temp.status = getStatus(temp.delivery_status);
                temp.action = getAction(temp.delivery_status);
            }
            return temp;
        }

        // lấy delivery_status
        public order get1Bill(string bill_id)
        {
            string sql = "select delivery_status from bill where bill_id = '"+bill_id+"'";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            order temp = new order();
            if (reader.Read()) 
            {
                temp.delivery_status = reader.GetInt32(0);
            }
            // close con
            reader.Close();
            cmd.Dispose();
            con.Close();
            return temp;
        }
        
        // lấy string status từ int delivery_status
        public string getStatus(int delivery_status)
        {
            if(delivery_status == 0)
            {
                return "Đang chờ xác nhận";
            }
            else if(delivery_status == 1)
            {
                return "Đã xác nhận, đang đóng gói";
            }
            else if(delivery_status == 2)
            {
                return "Đã đóng gói, đang vận chuyển";
            }
            else if(delivery_status == 3)
            {
                return "Đã giao hàng";
            }
            else if(delivery_status == -1)
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

        // thực thi hành động với cột delivery_status
        public bool changeDelivery_status(string bill_id)
        {
            order temp = get1Bill(bill_id);
            string sql = "";
            if (temp.delivery_status == 0)
            {
                sql = "update bill set delivery_status = 1 where bill_id = '" + bill_id + "'";
            }
            else if (temp.delivery_status == 1)
            {
                sql = "update bill set delivery_status = 2 where bill_id = '" + bill_id + "'";
            }
            else if (temp.delivery_status == 2)
            {
                sql = "update bill set delivery_status = 3 where bill_id = '" + bill_id + "'";
            }
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kq = cmd.ExecuteNonQuery();
            return kq > 0;
        }

        // admin hủy đơn
        public bool cancelOrder(string bill_id)
        {
            string sql = "update bill set delivery_status = -2 where bill_id = '"+bill_id+"'";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int kq = cmd.ExecuteNonQuery();
            return kq > 0;
        }
        
        /*
         ------------------------truy xuất dữ liệu bảng order_detail trong database------------------------
        */
        // lấy dữ liệu bảng order_detail
        public List<orders_detail> getOrderDetail(string bill_id)
        {
            string sql = "select * from order_detail where bill_id = '" + bill_id + "'";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            con.Open();
            cmd.Fill(dt);
            // close con
            cmd.Dispose();
            con.Close();

            List<orders_detail> list = new List<orders_detail>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                orders_detail temp = new orders_detail();
                temp.bill_id = Convert.ToInt32(dt.Rows[i]["bill_id"].ToString());
                temp.shoe_id = Convert.ToInt32(dt.Rows[i]["shoe_id"].ToString());
                temp.size = Convert.ToInt32(dt.Rows[i]["size"].ToString());
                temp.price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
                temp.quantity = Convert.ToInt32(dt.Rows[i]["quantity"].ToString());
                list.Add(temp);
            }
            return list;
        }

    }
}