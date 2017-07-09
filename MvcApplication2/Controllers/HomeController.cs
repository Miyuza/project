using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(String name, String email, String msg)
        {
            object data = null;
            try
            {
                    MailAddress From = new MailAddress(email, name);
                    String to = "me04se@gmail.com";
                    MailAddress To = new MailAddress(to);
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(From.Address, "3016218506")
                    };

                    using (var message = new MailMessage(From, To)
                    {
                        Subject = "Notice",
                        Body = msg
                    })
                    {
                        smtp.Send(message);
                    }
                    data = new
                    {
                        valid = true,

                    };
                } 
            catch (Exception e)
            {
                
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RequestRecipie()
        {
            List<Request> lst = DBHandler.DAL.requests();
            if (lst == null || lst.Count == 0)
            {
                ViewBag.list = "empty";
            }
            else
            {
                ViewBag.list = "notempty";
                lst.Reverse();
            }
            return View(lst);
        }
        [HttpGet]
        public ActionResult Recipie()
        {
            List<Recipie> lst = DBHandler.DAL.recipies();
            if (lst == null || lst.Count == 0)
            {
                ViewBag.list = "empty";
            }
            else
            {
                ViewBag.list = "notempty";
                lst.Reverse();
            }
            return View(lst);
        }
        public ActionResult LogOut()
        {
            Session["email"] = null;
            return View("LogIn");
        }
        [HttpPost]
        public ActionResult validate(String login, String password)
         {
            Object data = null;
            bool x = false;
            x = DBHandler.DAL.SignIn(login, password);
            Session["email"] = login;
            data = new
                {
                    valid = x,
                };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SignUp(String login, String password)
        {
            Object data = null;
            bool x = false;
            x = DBHandler.DAL.CheckLogin(login,password);
            if (x == false)
                DBHandler.DAL.SignUp(login, password);
            data = new
            {
                valid = x,
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadRequest(String rname, String request)
        {
            return View("Request");
        }
        [HttpPost]
        public ActionResult request(String rname, String request)
        {
            Object data = null;
            
            bool x = DBHandler.DAL.AddRequest(rname, request);
            data = new
            {
                valid = x,
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Recipie(String str)
        {
            List<Recipie> lst = DBHandler.DAL.search(str);
            if (lst == null || lst.Count == 0)
            {
                ViewBag.list = "empty";
            }
            else
            {
                ViewBag.list = "notempty";
                lst.Reverse();
            }
            return View(lst);
        }
    }
}
