using System.Web.Mvc;
//using Mhotivo.App_Data.Repositories;
//using Mhotivo.App_Data.Repositories.Interfaces;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Repositories;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;
using AutoMapper;
using Mhotivo.Encryption;

namespace Mhotivo.Controllers
{
    public class UserController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();
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

            ViewBag.RoleId = new SelectList(_roleRepository.Query(x => x), "Id", "Name", thisUser.Role.Id);

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
            if (myUser.Role.Id != modelUser.RoleId)
            {
                myUser.Role = _roleRepository.GetById(modelUser.RoleId);
                updateRole = true;
            }

            User user = _userRepository.Update(myUser, updateRole);
            const string title = "Usuario Actualizado";
            var content = "El usuario " + user.DisplayName + " - " + user.Email +
                             " ha sido actualizado exitosamente.";

            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            User user = _userRepository.Delete(id);

            const string title = "Usuario Eliminado";
            var content = "El usuario " + user.DisplayName + " - " + user.Email + " ha sido eliminado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

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
                             Password =modelUser.Password,
                             //Password = Md5CryptoService.EncryptData(modelUser.Password),
                             Role = _roleRepository.GetById(modelUser.Id),
                             Status = modelUser.Status
                         };

            var user = _userRepository.Create(myUser);

            const string title = "Usuario Agregado";
            var content = "El usuario " + user.DisplayName + " - " + user.Email + " ha sido agregado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);

            return RedirectToAction("Index");
        }
    }
}