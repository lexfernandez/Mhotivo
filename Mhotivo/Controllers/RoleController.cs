using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        //
        // GET: /Role/

        public ActionResult Index()
        {
            var message = (MessageModel) TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.Type;
                ViewBag.MessageTitle = message.Title;
                ViewBag.MessageContent = message.Content;
            }

            return View(_roleRepository.GetAllRoles());
        }

        //
        // GET: /Role/

        [HttpGet]
        public ActionResult Edit(long id)
        {
            Role r = _roleRepository.GetById(id);
            var role = new RoleEditModel
                       {
                           Id = r.Id,
                           Description = r.Description,
                           Name = r.Name
                       };

            return View("_Edit", role);
        }

        [HttpPost]
        public ActionResult Edit(Role modelRole)
        {
            Role role = _roleRepository.Update(modelRole);
            const string title = "Role Actualizado";
            string content = "El role " + role.Name + " ha sido modificado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "SUCCESS",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }
    }
}