using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using Mhotivo.App_Data;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class GroupController : Controller
    {
        private readonly MhotivoContext db = new MhotivoContext();

        //
        // GET: /Group/

        public ActionResult Index()
        {
            var message = (MessageModel) TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.Type;
                ViewBag.MessageTitle = message.Title;
                ViewBag.MessageContent = message.Content;
            }

            IQueryable<Group> groups = db.Groups.Select(x => x);
            return View(groups);
        }

        public JsonResult GetMembers(string filter)
        {
            var members =
                db.Users.Where(x => x.DisplayName.Contains(filter) || x.Email.Contains(filter))
                    .Select(x => new {name = x.DisplayName + " <" + x.Email + ">", value = x.Id})
                    .ToList();
            return Json(members, JsonRequestBehavior.AllowGet);
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
        public ActionResult Add(AddGroup group)
        {
            try
            {
                List<int> usersId = group.Users.Split(',').Select(Int32.Parse).ToList();
                IQueryable<User> users = db.Users.Where(x => usersId.Contains(x.Id));
                var g = new Group {Name = group.Name, Users = users.ToList()};

                if (ModelState.IsValid)
                {
                    db.Groups.Add(g);
                    db.SaveChanges();
                    TempData["MessageInfo"] = new MessageModel
                                              {
                                                  Type = "SUCCESS",
                                                  Title = "Grupo Agregado",
                                                  Content = "El grupo fue agregado exitosamente!"
                                              };
                }
                else
                {
                    TempData["MessageInfo"] = new MessageModel
                                              {
                                                  Type = "INFO",
                                                  Title = "Data validation",
                                                  Content = "The data is no valid!"
                                              };
                }
            }
            catch
            {
                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "ERROR",
                                              Title = "Error",
                                              Content = "Something went wrong, please try again!"
                                          };
            }
            IQueryable<Group> groups = db.Groups.Select(x => x);
            return RedirectToAction("Index", groups);
        }

        //
        // GET: /Group/Edit/5

        public ActionResult Edit(int id)
        {
            Group group = db.Groups.FirstOrDefault(x => x.Id.Equals(id));
            return View("Edit", group);
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, AddGroup group)
        {
            try
            {
                Group g = db.Groups.FirstOrDefault(x => x.Id.Equals(id));
                g.Name = group.Name;
                if (!group.Users.IsEmpty())
                {
                    List<int> usersId = group.Users.Split(',').Select(Int32.Parse).ToList();
                    IQueryable<User> users = db.Users.Where(x => usersId.Contains(x.Id));
                    g.Users = g.Users.Concat(users).ToList();
                }

                if (ModelState.IsValid)
                {
                    db.Entry(g).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MessageInfo"] = new MessageModel
                                              {
                                                  Type = "SUCCESS",
                                                  Title = "Grupo Editado",
                                                  Content = "El grupo fue editado exitosamente!"
                                              };
                }

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "ERROR",
                                              Title = "Error en edición",
                                              Content =
                                                  "El grupo no pudo ser editado correctamente, por favor intente nuevamente!"
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
                Group group = db.Groups.FirstOrDefault(x => x.Id == id);
                db.Groups.Remove(group);
                db.SaveChanges();

                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "SUCCESS",
                                              Title = "Grupo eliminado",
                                              Content = "Grupo eliminado exitosamente!"
                                          };

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MessageInfo"] = new MessageModel
                                          {
                                              Type = "ERROR",
                                              Title = "Error en eliminación",
                                              Content =
                                                  "El grupo no pudo ser eliminado correctamente, por favor intente nuevamente!"
                                          };
                return View("Index");
            }
        }

        [HttpPost]
        public bool DeleteUser(int id, int groupId)
        {
            try
            {
                db.Database.ExecuteSqlCommand("Delete From UserGroups where User_UserId=" + id + " and Group_Id=" +
                                              groupId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}