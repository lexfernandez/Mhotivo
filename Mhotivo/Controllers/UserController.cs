using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class UserController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public ActionResult Index()
        {
            var message = (MessageModel) TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.Type;
                ViewBag.MessageTitle = message.Title;
                ViewBag.MessageContent = message.Content;
            }

            return View(_userRepository.GetAllUsers());
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            User thisUser = _userRepository.GetById(id);
            var user = new UserEditModel
                       {
                           Email = thisUser.Email,
                           Id = thisUser.Id,
                           DisplayName = thisUser.DisplayName,
                           Password = thisUser.Password,
                           ConfirmPassword = thisUser.Password,
                           Status = thisUser.Status,
                           RoleId = thisUser.Role.Id
                       };

            ViewBag.Id = new SelectList(_roleRepository.Query(x => x), "Id", "Name", thisUser.Role.Id);

            return View("Edit", user);
        }

        [HttpPost]
        public ActionResult Edit(UserEditModel modelUser)
        {
            bool updateRole = false;
            User myUser = _userRepository.GetById(modelUser.Id);
            myUser.DisplayName = modelUser.DisplayName;
            myUser.Email = modelUser.Email;
            myUser.Password = modelUser.Password;
            myUser.Status = modelUser.Status;
            if (myUser.Role.Id != modelUser.Id)
            {
                myUser.Role = _roleRepository.GetById(modelUser.Id);
                updateRole = true;
            }

            User user = _userRepository.Update(myUser, updateRole);
            const string title = "Usuario Actualizado";
            string content = "El usuario " + user.DisplayName + " - " + user.Email +
                             " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            User user = _userRepository.Delete(id);

            const string title = "Usuario Eliminado";
            string content = "El usuario " + user.DisplayName + " - " + user.Email + " ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Id = new SelectList(_roleRepository.Query(x => x), "Id", "Name");
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
                             Role = _roleRepository.GetById(modelUser.Id),
                             Status = modelUser.Status
                         };

            User user = _userRepository.Create(myUser);
            const string title = "Usuario Agregado";
            string content = "El usuario " + user.DisplayName + " - " + user.Email + " ha sido agregado exitosamente.";
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