using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Mhotivo.App_Data;
using Mhotivo.Models;
using Microsoft.Ajax.Utilities;

namespace Mhotivo.Controllers
{
    public class GroupController : Controller
    {
        private MhotivoContext db = new MhotivoContext();

        //
        // GET: /Group/

        public ActionResult Index()
        {
            var message = (MessageModel)TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.MessageType;
                ViewBag.MessageTitle = message.MessageTitle;
                ViewBag.MessageContent = message.MessageContent;
            }

            var groups = db.Groups.Select(x => x);
            return View(groups);
        }

        public JsonResult GetMembers(string filter)
        {
            var members = db.Users.Where(x => x.DisplayName.Contains(filter) || x.Email.Contains(filter)).Select(x => new { name= x.DisplayName+" <"+x.Email+">",value=x.UserId }).ToList();
            return this.Json(members, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Group/Create

        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /Group/Create

        [HttpPost]
        public ActionResult Add(addGroup group)
        {
            try
            {
                var usersId = group.Users.Split(',').Select(Int32.Parse).ToList();
                IQueryable<User> users = db.Users.Where(x => usersId.Contains(x.UserId));
                var g = new Group {Name = group.Name, Users = users.ToList()};

                if (ModelState.IsValid)
                {
                    db.Groups.Add(g);
                    db.SaveChanges();
                    TempData["MessageInfo"] = new MessageModel
                    {
                        MessageType = "SUCCESS",
                        MessageTitle = "Grupo Agregado",
                        MessageContent = "El grupo fue agregado exitosamente!"
                    };
                }
                else
                {
                    TempData["MessageInfo"] = new MessageModel
                    {
                        MessageType = "INFO",
                        MessageTitle = "Data validation",
                        MessageContent = "The data is no valid!"
                    };
                }
                
            }
            catch (Exception exception)
            {
                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "ERROR",
                    MessageTitle = "Error",
                    MessageContent = "Something went wrong, please try again!"
                };                
            }
            var groups = db.Groups.Select(x => x);
            return RedirectToAction("Index",groups);

        }

        //
        // GET: /Group/Edit/5

        public ActionResult Edit(int id)
        {
            var group = db.Groups.FirstOrDefault(x => x.Id.Equals(id));
            return View("Edit", group);
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, addGroup group)
        {
            try
            {
                var g = db.Groups.FirstOrDefault(x => x.Id.Equals(id));
                g.Name = group.Name;
                if (!group.Users.IsEmpty())
                {
                    var usersId = group.Users.Split(',').Select(Int32.Parse).ToList();
                    IQueryable<User> users = db.Users.Where(x => usersId.Contains(x.UserId));
                    g.Users = g.Users.Concat(users).ToList();    
                }
                
                if (ModelState.IsValid)
                {
                    db.Entry(g).State= EntityState.Modified;
                    db.SaveChanges();
                    TempData["MessageInfo"] = new MessageModel
                    {
                        MessageType = "SUCCESS",
                        MessageTitle = "Grupo Editado",
                        MessageContent = "El grupo fue editado exitosamente!"
                    };
                }

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "ERROR",
                    MessageTitle = "Error en edición",
                    MessageContent = "El grupo no pudo ser editado correctamente, por favor intente nuevamente!"
                };
                return View("Index");
            }
        }

        //
        // POST: /Group/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var group= db.Groups.FirstOrDefault(x => x.Id == id);
                db.Groups.Remove(group);
                db.SaveChanges();

                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "SUCCESS",
                    MessageTitle = "Grupo eliminado",
                    MessageContent = "Grupo eliminado exitosamente!"
                };

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MessageInfo"] = new MessageModel
                {
                    MessageType = "ERROR",
                    MessageTitle = "Error en eliminación",
                    MessageContent = "El grupo no pudo ser eliminado correctamente, por favor intente nuevamente!"
                };
                return View("Index");
            }
        }

        [HttpPost]
        public bool DeleteUser(int id,int groupId)
        {
            try
            {
                db.Database.ExecuteSqlCommand("Delete From UserGroups where User_UserId=" + id + " and Group_Id=" + groupId);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
