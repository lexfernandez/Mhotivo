using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{

    public class NotificationController : Controller
    {

        public MhotivoContext db = new MhotivoContext();
        //
        // GET: /Notification/

        public ActionResult Index()
        {
            var notifications = db.Notifications.Select(x => x);
            return View();
        }

        //
        // GET: /Notification/Create

        public ActionResult Add()
        {
            return View("Create");
        }

        //
        // GET: /Notification/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // POST: /Notification/Create

        //[HttpPost]
        //public ActionResult Add(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Notification/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Notification/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Notification/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Notification/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
