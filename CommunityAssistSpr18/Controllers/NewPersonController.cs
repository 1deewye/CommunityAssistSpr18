using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistSpr18.Models;

namespace CommunityAssistSpr18.Controllers
{
    public class NewPersonController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: NewPerson
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include ="LastName, FirstName, Email, Phone, PlainPassword, Apartment, Street, City, State, Zipcode")]NewPerson np)
        {
            Message m =new Message();
            int result = db.usp_Register(np.LastName, np.FirstName, np.Email, np.PlainPassword, np.Apartment, np.Street, np.City, np.State, np.Zipcode, np.Phone);
            if (result != -1)
            {
                m.MessageText = "Welcome, " + np.FirstName;
            }
            else
            {
                m.MessageText = "Something went wrong";
            }
            return View("Result", m);
        }
        public ActionResult Result(Message m)
        {
            return View(m);
        }

    }
}