using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data;
using Mhotivo.Models;
using Newtonsoft.Json.Serialization;

namespace Mhotivo.Controllers
{

    public class NotificationController : Controller
    {

        public MhotivoContext db = new MhotivoContext();
        //
        // GET: /Notification/

        public ActionResult Index()
        {
            var message = (MessageModel)TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.MessageType;
                ViewBag.MessageTitle = message.MessageTitle;
                ViewBag.MessageContent = message.MessageContent;
            }
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
            if (ModelState.IsValid)
            {

                var template = new Notification
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
            }
            else
            {
                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "ERROR",
                    MessageTitle = "",
                    MessageContent = ""
                }; 
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetGroupsAndEmails(string filter)
        {
             var groups= db.Groups.Where(x => x.Name.Contains(filter)).Select(x =>  x.Name).ToList();
            var mails = db.Users.Where(x => x.DisplayName.Contains(filter) || x.Email.Contains(filter)).Select( x => x.Email ).ToList();
            groups= groups.Union(mails).ToList();
            return this.Json(groups,JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Notification/Edit/5

        public ActionResult Edit(int id)
        {
            Notification toEdit = db.Notifications.FirstOrDefault(x => x.Id.Equals(id));
            return View(toEdit);
        }

        //
        // POST: /Notification/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Notification notification)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(notification).State=EntityState.Modified;
                    db.SaveChanges();
                    TempData["MessageInfo"] = new MessageModel
                    {
                        MessageType = "SUCCESS",
                        MessageTitle = "Notificación Editada",
                        MessageContent = "La notificación fue editada exitosamente!"
                    };
                }

                
            }
            catch (Exception e)
            {
                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "ERROR",
                    MessageTitle = "Error en edición",
                    MessageContent = "La notificación no pudo ser editada correctamente, por favor intente nuevamente!"
                };
            }
            var g = db.Groups.Select(x => x);
            return RedirectToAction("Index",g);
        }


        //
        // POST: /Notification/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var toDelete = db.Notifications.FirstOrDefault(x => x.Id.Equals(id));
                db.Notifications.Remove(toDelete);
                db.SaveChanges();
                var notifications = db.Notifications.Select(x => x.Id);
                return RedirectToAction("Index",notifications);
            }
            catch
            {
                return View("Index");
            }
        }
    }

}
