using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleRepository _roleRepo = RoleRepository.Instance;
                //
        // GET: /Role/

        public ActionResult Index()
        {
            var message = (MessageModel)TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.MessageType;
                ViewBag.MessageTitle = message.MessageTitle;
                ViewBag.MessageContent = message.MessageContent;
            }

            return View(_roleRepo.Query(x => x));
        }

        //
        // GET: /Role/

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var r = _roleRepo.GetById(id);
            var role = new RoleEditModel
                   {
                       RoleId = r.RoleId,
                       Description = r.Description,
                       Name = r.Name
                   };

            return View("_Edit", role);
        }

        [HttpPost]
        public ActionResult Edit(Role modelRole)
        {
            var role = _roleRepo.UpdateNew(modelRole);
            const string title = "Role Actualizado";
            var content = "El role " + role.Name + " ha sido modificado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "SUCCESS",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Index");
        }
    }
}
