using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class MeisterController : Controller
    {
        private readonly IMeisterRepository _meisterRepository;
        private readonly IContactInformationRepository _contactInformationRepository;

        public MeisterController(IMeisterRepository meisterRepository, IContactInformationRepository contactInformationRepository)
        {
            _meisterRepository = meisterRepository;
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

            return View(_meisterRepository.GetAllMeisters());
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
                Controller = "Meister"
            };

            return View("ContactEdit", contactInformation);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var meister = _meisterRepository.GetMeisterEditModelById(id);

            return View("Edit", meister);
        }

        [HttpPost]
        public ActionResult Edit(MeisterEditModel modelMeister)
        {
            var myMeister = _meisterRepository.GetById(modelMeister.Id);
            _meisterRepository.UpdateMeisterFromMeisterEditModel(modelMeister, myMeister);

            const string title = "Maestro Actualizado";
            var content = "El maestro " + myMeister.FullName + " ha sido actualizado exitosamente.";

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
            var meister = _meisterRepository.Delete(id);

            const string title = "Maestro Eliminado";
            var content = "El maestro " + meister.FullName + " ha sido eliminado exitosamente.";
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
                Controller = "Meister"
            };
            return View("ContactAdd", model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(MeisterRegisterModel modelMeister)
        {
            var myMeister = _meisterRepository.GenerateMeisterFromRegisterModel(modelMeister);

            _meisterRepository.Create(myMeister);
            const string title = "Maestro Agregado";
            var content = "El maestro " + myMeister.FullName + "ha sido agregado exitosamente.";
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
            var meister = _meisterRepository.GetMeisterDisplayModelById(id);

            return View("Details", meister);
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            var meister = _meisterRepository.GetMeisterEditModelById(id);
            
            return View("DetailsEdit", meister);
        }

        [HttpPost]
        public ActionResult DetailsEdit(MeisterEditModel modelMeister)
        {
            var myMeister = _meisterRepository.GetById(modelMeister.Id);
            _meisterRepository.UpdateMeisterFromMeisterEditModel(modelMeister, myMeister);
            
            const string title = "Maestro Actualizado";
            var content = "El maestro " + myMeister.FullName + " ha sido actualizado exitosamente.";

            TempData["MessageInfo"] = new MessageModel
            {
                MessageType = "INFO",
                MessageTitle = title,
                MessageContent = content
            };

            return RedirectToAction("Details/" + modelMeister.Id);
        }
    }
}
