using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class Home2Controller : Controller
    {
        //
        // GET: /Home2/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyRecipies()
        {
            String email = Session["email"].ToString();
            List<Recipie> lst = DBHandler.DAL.Myrecipies(email);
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
        public ActionResult LogIn()
        {
            return View();
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
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Editt(String id)
        {
            int i = Convert.ToInt32(id);
            Recipie obj = DBHandler.DAL.search(i);
            return RedirectToAction("Edit", "Home2", obj);
        }
        [HttpPost]
        public ActionResult Edit(String iid)
        {
            int i = Convert.ToInt32(iid);
            Recipie obj = DBHandler.DAL.search(i);
            return View(obj);
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
        public ActionResult UploadRecipie(String rname,String recipie)
        {
            if (Request.Files["Image"] != null && rname != null && recipie != null && rname != "" && recipie != "")
            {
                String guid = Guid.NewGuid().ToString();
                String[] str = guid.Split('-');
                var file = Request.Files["Image"];
                var name = str[0] + file.FileName;
                var path = Server.MapPath("~/UploadedImages");
                var filepath = System.IO.Path.Combine(path, name);
                file.SaveAs(filepath);
                String email = Session["email"].ToString();
                if (file.FileName == null || file.FileName == "")
                    name = "";
                bool x = DBHandler.DAL.AddRecipie(email, rname, recipie, name);
                if (x == true)
                    ViewBag.upload = "1";
                else
                    ViewBag.upload = "2";

            }
            else
                ViewBag.upload = "3";
            return View("Add");
        }
        public ActionResult UpdateRecipie(String id,String rname, String recipie)
        {
            if (rname != null && recipie != null && rname != "" && recipie != "")
            {
                String guid = Guid.NewGuid().ToString();
                String[] str = guid.Split('-');
                var file = Request.Files["Image"];
                String st = null;
                if (Request.Files["Image"] == null || file.FileName == "")
                {
                    st = DBHandler.DAL.searchImg(Convert.ToInt32(id));
                }
                else
                {
                    var name = str[0] + file.FileName;
                    st = name;
                    var path = Server.MapPath("~/UploadedImages");
                    var filepath = System.IO.Path.Combine(path, name);
                    file.SaveAs(filepath);
                }
                String email = Session["email"].ToString();
                bool x = DBHandler.DAL.updateRecipie(Convert.ToInt32(id),email, rname, recipie, st);
                if (x == true)
                    ViewBag.upload = "1";
                else
                    ViewBag.upload = "2";

            }
            else
                ViewBag.upload = "3";
            return View("Index");
        }
        public ActionResult Search(String recipie)
        {
            return View("Recipie");
        }
        [HttpPost]
        public ActionResult UploadRequest(String rname, String request)
        {
            return View("Request");
        }
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
        public ActionResult DelRecipie(String id)
        {
            Object data = null;

            DBHandler.DAL.delRecipie(Convert.ToInt32(id));
            data = new
            {
                valid = true,
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
