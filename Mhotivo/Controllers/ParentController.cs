using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class ParentController : Controller
    {
        private readonly IParentRepository _parentRepository;
        private readonly IContactInformationRepository _contactInformationRepository;

        public ParentController(IParentRepository parentRepository, IContactInformationRepository contactInformationRepository)
        {
            _parentRepository = parentRepository;
            _contactInformationRepository = contactInformationRepository;
        }

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

            return View(_parentRepository.GetAllParents());
        }

        [HttpGet]
        public ActionResult ContactEdit(long id)
        {
            var thisContactInformation = _contactInformationRepository.GetById(id);
            var contactInformation = new ContactInformationEditModel
            {
                Type = thisContactInformation.Type,
                Value = thisContactInformation.Value,
                Id = thisContactInformation.ContactId,
                people = thisContactInformation.People,
                Controller = "Parent"
            };

            return View("ContactEdit", contactInformation);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var parent = _parentRepository.GetParentEditModelById(id);

            return View("Edit", parent);
        }

        [HttpPost]
        public ActionResult Edit(ParentEditModel modelParent)
        {
            var myParent = _parentRepository.GetById(modelParent.Id);

            _parentRepository.UpdateParentFromParentEditModel(modelParent, myParent);

            const string title = "Padre o Tutor Actualizado";
            var content = "El Padre o Tutor " + myParent.FullName + " ha sido actualizado exitosamente.";

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
            var parent = _parentRepository.Delete(id);

            const string title = "Padre o Tutor Eliminado";
            var content = "El Padre o Tutor " + parent.FullName + " ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ContactAdd(long id)
        {
            var model = new ContactInformationRegisterModel
            {
                PeopleId = (int)id,
                Controller = "Parent"
            };
            return View("ContactAdd", model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(ParentRegisterModel modelParent)
        {
            var myParent = _parentRepository.GenerateParentFromRegisterModel(modelParent);
            
            var parent = _parentRepository.Create(myParent);
            const string title = "Padre o Tutor Agregado";
            var content = "El Padre o Tutor " + myParent.FullName + "ha sido agregado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "SUCCESS",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var parent = _parentRepository.GetParentDisplayModelById(id);

            return View("Details", parent);
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            var parent = _parentRepository.GetParentEditModelById(id);
            
            return View("DetailsEdit", parent);
        }

        [HttpPost]
        public ActionResult DetailsEdit(ParentEditModel modelParent)
        {
            var myParent = _parentRepository.GetById(modelParent.Id);
            _parentRepository.UpdateParentFromParentEditModel(modelParent, myParent);
            
            const string title = "Padre o Tutor Actualizado";
            var content = "El Padre o Tutor " + myParent.FullName + " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Details/" + modelParent.Id);
        }
    }
}
