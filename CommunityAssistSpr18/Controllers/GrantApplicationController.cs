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
        public ActionResult Index([Bind(Include = "GrantTypeKey, GrantTypeName, GrantTypeDescription, GrantTypeMaximum, GrantTypeLifetimeMaximum, GrantTypeDateEntered")]GrantType g)
        {
            try
            {
                g.GrantTypeKey = (int)Session["PersonKey"];
                g.GrantTypeDateEntered = DateTime.Now;
                db.GrantTypes.Add(g);
                db.SaveChanges();
                Message m = new Message();
                m.MessageText = "We will get back to you shortly";
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