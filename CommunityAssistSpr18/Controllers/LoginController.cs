using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistSpr18.Models;

namespace CommunityAssistSpr18.Controllers
{
    public class LoginController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include="Email, Password")]LoginClass lc)
        {
            int results = db.usp_Login(lc.Email, lc.Password);
            int varKey = 0;
            Message msg = new Message();
            if(results != -1)
            {
                var pkey = (from r in db.People
                           where r.PersonEmail.Equals(lc.Email)
                           select r.PersonKey).FirstOrDefault();
                varKey = (int)pkey;
                Session["PersonKey"] = varKey;

                msg.MessageText = "Welcome, " + lc.Email;

            }
            else
            {
                msg.MessageText = "Invalid Login";
            }
            return View("result", msg);
        }

        public ActionResult Result(Message msg)
        {
            return View(msg);
        }
    }
}