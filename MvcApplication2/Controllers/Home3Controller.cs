using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class Home3Controller : Controller
    {
        //
        // GET: /Home3/

        public ActionResult Recipie(int ID)
        {
            Recipie obj = DBHandler.DAL.search(ID);
            return View(obj);
        }

    }
}
