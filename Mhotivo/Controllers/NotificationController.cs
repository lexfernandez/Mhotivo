using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            var message = (MessageModel) TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.Type;
                ViewBag.MessageTitle = message.Title;
                ViewBag.MessageContent = message.Content;
            }
            IQueryable<Notification> notifications = db.Notifications.Where(x => true);
            return View(notifications);
        }

        //
        // GET: /Notification/Create
        [HttpGet]
        public ActionResult Add()
        {
            var notification = new Notification();
            ;
            return View("Add", notification);
        }

        [HttpPost]
        public ActionResult Add(Notification eventNotification)
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
                string content = "El evento " + eventNotification.EventName + " ha sido agregado exitosamente.";
                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "SUCCESS",
                                              Title = title,
                                              Content = content
                                          };
            }
            else
            {
                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "ERROR",
                                              Title = "",
                                              Content = ""
                                          };
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetGroupsAndEmails(string filter)
        {
            List<string> groups = db.Groups.Where(x => x.Name.Contains(filter)).Select(x => x.Name).ToList();
            List<string> mails =
                db.Users.Where(x => x.DisplayName.Contains(filter) || x.Email.Contains(filter))
                    .Select(x => x.Email)
                    .ToList();
            groups = groups.Union(mails).ToList();
            return Json(groups, JsonRequestBehavior.AllowGet);
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
                    db.Entry(notification).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MessageInfo"] = new MessageModel
                                              {
                                                  Type = "SUCCESS",
                                                  Title = "Notificación Editada",
                                                  Content = "La notificación fue editada exitosamente!"
                                              };
                }
            }
            catch
            {
                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "ERROR",
                                              Title = "Error en edición",
                                              Content =
                                                  "La notificación no pudo ser editada correctamente, por favor intente nuevamente!"
                                          };
            }
            IQueryable<Group> g = db.Groups.Select(x => x);
            return RedirectToAction("Index", g);
        }


        //
        // POST: /Notification/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Notification toDelete = db.Notifications.FirstOrDefault(x => x.Id.Equals(id));
                db.Notifications.Remove(toDelete);
                db.SaveChanges();
                IQueryable<long> notifications = db.Notifications.Select(x => x.Id);
                return RedirectToAction("Index", notifications);
            }
            catch
            {
                return View("Index");
            }
        }
    }
}