using System.Linq;
using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepo = UserRepository.Instance;
        private readonly RoleRepository _roleRepo = RoleRepository.Instance;

        [AllowAnonymous]
        public ActionResult Index()
        {
            var message = (MessageModel)TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.MessageType;
                ViewBag.MessageTitle = message.MessageTitle;
                ViewBag.MessageContent = message.MessageContent;
            }

            return View(_userRepo.Query(x => x).ToList()
                .Select(x => new DisplayUserModel
                {
                    DisplayName = x.DisplayName,
                    Email = x.Email,
                    Role = x.Role.Name,
                    Status = _userRepo.ActiveUserDisplaytext(x.Status),
                    UserId = x.UserId
                }));   
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var thisUser = _userRepo.GetById(id);
            var user = new UserEditModel
            {
                Email = thisUser.Email, 
                Id = thisUser.UserId, 
                DisplayName = thisUser.DisplayName,
                Password = thisUser.Password,
                ConfirmPassword = thisUser.Password,
                Status = thisUser.Status,
                RoleId = thisUser.Role.RoleId
            };
            
            ViewBag.RoleId = new SelectList(_roleRepo.Query(x => x), "RoleId", "Name", thisUser.Role.RoleId);

            return View("Edit", user);
        }

        [HttpPost]
        public ActionResult Edit(UserEditModel modelUser)
        {
            var updateRole = false;
            var myUser = _userRepo.GetById(modelUser.Id);
            myUser.DisplayName = modelUser.DisplayName;
            myUser.Email = modelUser.Email;
            myUser.Password = modelUser.Password;
            myUser.Status = modelUser.Status;
            if (myUser.Role.RoleId != modelUser.RoleId)
            {
                myUser.Role = _roleRepo.GetById(modelUser.RoleId);
                updateRole = true;
            }
            
            var user = _userRepo.Update(myUser, updateRole);
            const string title = "Usuario Actualizado";
            var content = "El usuario " + user.DisplayName + " - " + user.Email + " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            var user = _userRepo.Delete(id);

            const string title = "Usuario Eliminado";
            var content = "El usuario " + user.DisplayName + " - " + user.Email + " ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO", MessageTitle = title, MessageContent = content
            };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.RoleId = new SelectList(_roleRepo.Query(x => x), "RoleId", "Name");
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(UserRegisterModel modelUser)
        {
            var myUser = new User
            {
                DisplayName = modelUser.DisplaName,
                Email = modelUser.UserName,
                Password = modelUser.Password,
                Role = _roleRepo.GetById(modelUser.RoleId),
                Status = modelUser.Status
            };

            var user = _userRepo.Create(myUser);
            const string title = "Usuario Agregado";
            var content = "El usuario " + user.DisplayName + " - " + user.Email + " ha sido agregado exitosamente.";
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
