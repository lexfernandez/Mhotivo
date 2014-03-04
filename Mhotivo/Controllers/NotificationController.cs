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
            var notifications = db.Notifications.Where(x => true);
            return View(notifications);
        }

        //
        // GET: /Notification/Create
        [HttpGet]
        public ActionResult Add()
        {
            var notification=new Notification();;
            return View("Add",notification);
        }

        [HttpPost]
        public ActionResult Add( Notification eventNotification)
        {
            var template= new Notification
                          {
                              EventName = eventNotification.EventName,
                              From = eventNotification.From,
                              To = eventNotification.To,
                              WithCopyTo = eventNotification.WithCopyTo,
                              WithHiddenCopyTo = eventNotification.WithHiddenCopyTo,
                              Subject = eventNotification.Subject,
                              Message = eventNotification.Message,
                              Created = DateTime.Now
                          };
            db.Notifications.Add(template);
            db.SaveChanges();
            const string title = "Notificación Agregado";
            var content = "El evento " + eventNotification.EventName + " ha sido agregado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "SUCCESS",
                MessageTitle = title,
                MessageContent = content
            };
            return RedirectToAction("Index");
        }

        public JsonResult GetGroupsAndEmails(string filter)
        {
            IQueryable<string> groupsName = db.Groups.Where(x => x.Name.Contains(filter)).Select(x => x.Name);
            IQueryable<string> groupsNameAndUserEmails = db.Users.Where(x => x.DisplayName.Contains(filter) || x.Email.Contains(filter))
                .Select(x => x.Email)
                .Union(groupsName);
            return this.Json(groupsNameAndUserEmails);
        }

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
