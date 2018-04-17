using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistSpr18.Models;

namespace CommunityAssistSpr18.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();
            return View(db.GrantTypes.ToList());
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}