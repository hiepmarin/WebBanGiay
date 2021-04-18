using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class taikhoanQuery
    {
        private DBConnection db;

        public taikhoanQuery()
        {
            db = new DBConnection();
        }

        public bool TryLogin(string username, string password)
        {
            string sql = "select * from account where username = '" + username + "' and password = '" + password + "'";

            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            con.Open();
            cmd.Fill(dt);
            con.Close();

            if (dt.Rows.Count <= 0)
            {
                return false;
            }

            return true;
        }

        public bool AdminTryLogin(string username, string password)
        {
            string sql = "select * from admin_account where username = '" + username + "' and password = '" + password + "'";

            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            con.Open();
            cmd.Fill(dt);
            con.Close();

            if (dt.Rows.Count <= 0)
            {
                return false;
            }

            return true;
        }

        public taikhoan getInfo(string username)
        {
            string sql = "select * from account where username = '" + username + "'";

            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            con.Open();
            cmd.Fill(dt);
            cmd.Dispose();
            con.Close();

            taikhoan a = new taikhoan();
            a.acc_id = Convert.ToInt32(dt.Rows[0]["acc_id"].ToString());
            a.name = dt.Rows[0]["name"].ToString();
            a.username = dt.Rows[0]["username"].ToString();
            a.password = dt.Rows[0]["password"].ToString();
            a.phone = dt.Rows[0]["phone"].ToString();
            a.email = dt.Rows[0]["email"].ToString();
            a.address = dt.Rows[0]["address"].ToString();
            a.birth = Convert.ToDateTime(dt.Rows[0]["birth"].ToString());

            return a;
        }

        public taikhoan getAdminInfo(string username)
        {
            string sql = "select * from admin_account where username = '" + username + "'";

            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            con.Open();
            cmd.Fill(dt);
            cmd.Dispose();
            con.Close();

            taikhoan a = new taikhoan();
            a.acc_id = Convert.ToInt32(dt.Rows[0]["id"].ToString());
            a.name = dt.Rows[0]["name"].ToString();
            a.username = dt.Rows[0]["username"].ToString();
            a.password = dt.Rows[0]["password"].ToString();

            return a;
        }

        public bool checkAccount(string username)
        {
            string sql = "select * from account where username = '" + username + "'";

            SqlConnection con = db.GetConnection();

            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            con.Open();
            cmd.Fill(dt);
            cmd.Dispose();
            con.Close();

            taikhoan a = new taikhoan();
            //a.TaiKhoan = dt.Rows[0]["TaiKhoan"].ToString();

            if (dt.Rows[0]["TaiKhoan"].ToString() != null)
            {
                return true;
            }
            return false;
        }
        
        public bool CreateAccount(taikhoan acc)
        {
            int kq = 0;
            string sql = "If not Exists (Select * from account where username = '" + acc.username + "' ) begin insert into " +
                "        account(name,username,password,phone,email,address,birth) values (N'" +
                         acc.name + "','" + acc.username + "','" + acc.password + "','" + acc.phone + "',N'" +
                         acc.email + "','" + acc.address + "','" + acc.birth + "') end";

            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            try
            {
                kq = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                if(ex.ToString() != null)
                {
                    kq = -1;
                }
            }
            finally
            {
                con.Close();
            }
            return kq > 0;
        }
        
        public bool EditAccount(taikhoan acc)
        {
            int kq = 0;
            string sql = "Update account set name = '"+acc.name+ "', password = '"+acc.password+ "', phone = '"+acc.phone+"'," +
                " email = '"+acc.email+ "', address = '"+acc.address+ "', birth = '"+acc.birth+"' where username = '"+acc.username+"'";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            try
            {
                kq = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                if(ex.ToString() != null)
                {
                    kq = -1;
                }
            }
            finally
            {
                con.Close();
            }
            return kq > 0;
        }
        
        public bool Order(string acc_id)
        {
            string sql = "select * from bill where acc_id = '" + acc_id + "'";
            SqlConnection con = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();

            return true;
        }
    }
}