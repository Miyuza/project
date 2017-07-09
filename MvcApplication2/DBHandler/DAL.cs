using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcApplication2.DBHandler
{
    public class DAL
    {
        public static void SignUp(String email,String pass)
        {
            String q = "";
            q = String.Format("Insert into [Table] (Email,Password) values('" + email + "','" + pass + "')");
            using (DBHelper db = new DBHelper())
            {
                db.ExecuteQuery(q);
            }
        }
        public static bool CheckLogin(String email, String pass)
        {
            String q = "";
            q = String.Format("Select * from [Table] where Email = '"+email+"'");
            using (DBHelper db = new DBHelper())
            {
                try
                {
                    SqlDataReader dr = db.ExecuteReader(q);
                    if (dr.Read())
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        public static bool SignIn(String email, String pass)
        {
            String q = "";
            q = String.Format("Select * from [Table] where Email = '" + email + "' and Password = '"+pass+"'");
            using (DBHelper db = new DBHelper())
            {
                SqlDataReader dr = db.ExecuteReader(q);
                if (dr.Read())
                    return true;
                else
                    return false;
            }
        }
        public static bool AddRecipie(String email, String name,String recip,String img)
        {
            String q = "";
            q = String.Format("Insert into Recipie (Email,Name,Recip,Img) values('" + email + "','" + name + "','"+recip+"','"+img+"')");
            try
            {
                using (DBHelper db = new DBHelper())
                {
                    db.ExecuteQuery(q);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool updateRecipie(int id,String email, String name, String recip, String img)
        {
            String q = "";
            q = String.Format("update Recipie set Email = '"+email+"',Name = '"+name+"',Recip = '"+recip+"',Img = '"+img+"' where ID = "+id);
            try
            {
                using (DBHelper db = new DBHelper())
                {
                    db.ExecuteQuery(q);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool delRecipie(int id)
        {
            String q = "";
            q = String.Format("Delete from Recipie where ID = "+id);
            try
            {
                using (DBHelper db = new DBHelper())
                {
                    db.ExecuteQuery(q);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool AddRequest(String rname, String request)
        {
            String q = "";
            q = String.Format("Insert into Request (Name,Req) values('" + rname + "','" + request + "')");
            try
            {
                using (DBHelper db = new DBHelper())
                {
                    db.ExecuteQuery(q);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static List<Recipie> recipies()
        {
            List<Recipie> lst = new List<Recipie>();
            Recipie obj = null;
            String q = "";
            q = String.Format("Select * from Recipie");
            try
            {
                using (DBHelper db = new DBHelper())
                {
                    SqlDataReader dr = db.ExecuteReader(q);
                    while (dr.Read())
                    {
                        obj = new Recipie();
                        obj.ID = dr.GetInt32(0);
                        obj.Email = dr.GetString(1);
                        obj.Name = dr.GetString(2);
                        obj.Recip = dr.GetString(3);
                        obj.img = dr.GetString(4);
                        lst.Add(obj);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return lst;
        }
        public static List<Recipie> Myrecipies(String email)
        {
            List<Recipie> lst = new List<Recipie>();
            Recipie obj = null;
            String q = "";
            q = String.Format("Select * from Recipie where Email = '"+email+"'");
            try
            {
                using (DBHelper db = new DBHelper())
                {
                    SqlDataReader dr = db.ExecuteReader(q);
                    while (dr.Read())
                    {
                        obj = new Recipie();
                        obj.ID = dr.GetInt32(0);
                        obj.Email = dr.GetString(1);
                        obj.Name = dr.GetString(2);
                        obj.Recip = dr.GetString(3);
                        obj.img = dr.GetString(4);
                        lst.Add(obj);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return lst;
        }
        public static List<Request> requests()
        {
            List<Request> lst = new List<Request>();
            Request obj = null;
            String q = "";
            q = String.Format("Select * from Request");
            try
            {
                using (DBHelper db = new DBHelper())
                {
                    SqlDataReader dr = db.ExecuteReader(q);
                    while (dr.Read())
                    {
                        obj = new Request();
                        obj.ID = dr.GetInt32(0);
                        obj.Name = dr.GetString(1);
                        obj.Req = dr.GetString(2);
                        lst.Add(obj);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return lst;
        }
        public static List<Recipie> search(String str)
        {
            List<Recipie> lst = new List<Recipie>();
            Recipie obj = null;
            String q = "";
            String[] sp = str.Split(' ');
            int size = sp.Length;
            String where = " where";
            int i = 0; 
            while (i < size)
            {

                where += " lower(Name) like '%" + sp[i].ToLower() + "%'";
                i++;
                if (i < size)
                    where += " OR";
            }

            q = String.Format("Select * from Recipie");
            q += where;
            try
            {
                using (DBHelper db = new DBHelper())
                {
                    SqlDataReader dr = db.ExecuteReader(q);
                    while (dr.Read())
                    {
                        obj = new Recipie();
                        obj.ID = dr.GetInt32(0);
                        obj.Email = dr.GetString(1);
                        obj.Name = dr.GetString(2);
                        obj.Recip = dr.GetString(3);
                        obj.img = dr.GetString(4);
                        lst.Add(obj);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return lst;
        }
        public static Recipie search(int id)
        {
            
            Recipie obj = null;
            String q = "";
           
            
            q = String.Format("Select * from Recipie where ID="+id);
            
            try
            {
                using (DBHelper db = new DBHelper())
                {
                    SqlDataReader dr = db.ExecuteReader(q);
                    if (dr.Read())
                    {
                        obj = new Recipie();
                        obj.ID = dr.GetInt32(0);
                        obj.Email = dr.GetString(1);
                        obj.Name = dr.GetString(2);
                        obj.Recip = dr.GetString(3);
                        obj.img = dr.GetString(4);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return obj;
        }
        public static String searchImg(int id)
        {

            Recipie obj = null;
            String q = "";

            String im = null;
            q = String.Format("Select * from Recipie where ID=" + id);

            try
            {
                using (DBHelper db = new DBHelper())
                {
                    SqlDataReader dr = db.ExecuteReader(q);
                    if (dr.Read())
                    {
                        obj = new Recipie();
                        obj.ID = dr.GetInt32(0);
                        obj.Email = dr.GetString(1);
                        obj.Name = dr.GetString(2);
                        obj.Recip = dr.GetString(3);
                        obj.img = dr.GetString(4);
                        im = obj.img;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return im;
        }
    }
}