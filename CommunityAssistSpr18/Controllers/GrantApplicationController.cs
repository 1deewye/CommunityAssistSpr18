using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistSpr18.Models;

namespace CommunityAssistSpr18.Controllers
{
    public class GrantApplicationController : Controller
    {
        // GET: GrantApplication
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        public ActionResult Index()
        {
            if (Session["PersonKey"]==null)
            {
                Message msg = new Message();
                msg.MessageText = "You must be logged in to apply";
                return RedirectToAction("Result", msg);
            }
            ViewBag.GrantList = new SelectList(db.GrantTypes, "GrantTypeKey", "GrantTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PersonKey, GrantTypeKey, GrantApplicationDate, GrantApplicationReason, GrantApplicationRequestedAmount, GrantApplicationStatusKey ")]GrantApplication g)
        {
            try
            {
                g.PersonKey = (int)Session["PersonKey"];
                g.GrantAppicationDate = DateTime.Now;
                g.GrantApplicationStatusKey = 1;
                db.SaveChanges();
                Message m = new Message("Thank you. We have recieved your application.");
                return RedirectToAction("Result", m);
            }
            catch (Exception e)
            {
                Message m = new Message();
                m.MessageText = e.Message;
                return RedirectToAction("Result", m);
            }
        }
        public ActionResult Result(Message msg)
        {
            return View(msg);
        }
    }

}