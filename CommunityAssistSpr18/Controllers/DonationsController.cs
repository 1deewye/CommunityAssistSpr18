using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistSpr18.Models;

namespace CommunityAssistSpr18.Controllers
{
    public class DonationsController : Controller
    {
        // GET: Donations
        public ActionResult Index()
        {
            if (Session["PersonKey"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "DonationKey, DonationDate, DonationAmount, DonationConfirmation")]Donation d)

        {
            d.DonationConfirmationCode = Guid.NewGuid();
            d.PersonKey = (int)Session["PersonKey"];
            d.DonationDate = DateTime.Now;
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();
            db.Donations.Add(d);
            db.SaveChanges();
            Message m = new Message("There is a new donation.");
            return View("Result", m);
        }
        public ActionResult Result(Message msg)
        { 
            return View(msg);
        }

    }
}