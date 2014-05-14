using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class ParentController : Controller
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IParentRepository _parentRepository;

        public ParentController(IParentRepository parentRepository,
            IContactInformationRepository contactInformationRepository)
        {
            _parentRepository = parentRepository;
            _contactInformationRepository = contactInformationRepository;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var message = (MessageModel) TempData["MessageInfo"];

            if (message != null)
            {
                ViewBag.MessageType = message.Type;
                ViewBag.MessageTitle = message.Title;
                ViewBag.MessageContent = message.Content;
            }

            return View(_parentRepository.GetAllParents());
        }

        [HttpGet]
        public ActionResult ContactEdit(long id)
        {
            ContactInformation thisContactInformation = _contactInformationRepository.GetById(id);
            var contactInformation = new ContactInformationEditModel
                                     {
                                         Type = thisContactInformation.Type,
                                         Value = thisContactInformation.Value,
                                         Id = thisContactInformation.Id,
                                         People = thisContactInformation.People,
                                         Controller = "Parent"
                                     };

            return View("ContactEdit", contactInformation);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            ParentEditModel parent = _parentRepository.GetParentEditModelById(id);

            return View("Edit", parent);
        }

        [HttpPost]
        public ActionResult Edit(ParentEditModel modelParent)
        {
            Parent myParent = _parentRepository.GetById(modelParent.Id);

            _parentRepository.UpdateParentFromParentEditModel(modelParent, myParent);

            const string title = "Padre o Tutor Actualizado";
            string content = "El Padre o Tutor " + myParent.FullName + " ha sido actualizado exitosamente.";

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
            Parent parent = _parentRepository.Delete(id);

            const string title = "Padre o Tutor Eliminado";
            string content = "El Padre o Tutor " + parent.FullName + " ha sido eliminado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ContactAdd(long id)
        {
            var model = new ContactInformationRegisterModel
                        {
                            Id = (int) id,
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
            Parent myParent = _parentRepository.GenerateParentFromRegisterModel(modelParent);

            Parent parent = _parentRepository.Create(myParent);
            const string title = "Padre o Tutor Agregado";
            string content = "El Padre o Tutor " + myParent.FullName + "ha sido agregado exitosamente.";
            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "SUCCESS",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            DisplayParentModel parent = _parentRepository.GetParentDisplayModelById(id);

            return View("Details", parent);
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            ParentEditModel parent = _parentRepository.GetParentEditModelById(id);

            return View("DetailsEdit", parent);
        }

        [HttpPost]
        public ActionResult DetailsEdit(ParentEditModel modelParent)
        {
            Parent myParent = _parentRepository.GetById(modelParent.Id);
            _parentRepository.UpdateParentFromParentEditModel(modelParent, myParent);

            const string title = "Padre o Tutor Actualizado";
            string content = "El Padre o Tutor " + myParent.FullName + " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
                                      {
                                          Type = "INFO",
                                          Title = title,
                                          Content = content
                                      };

            return RedirectToAction("Details/" + modelParent.Id);
        }
    }
}