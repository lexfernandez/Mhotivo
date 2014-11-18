using System.Web.Mvc;
using Mhotivo.App_Data.Repositories;
using Mhotivo.App_Data.Repositories.Interfaces;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class MeisterController : Controller
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IMeisterRepository _meisterRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public MeisterController(IMeisterRepository meisterRepository,
            IContactInformationRepository contactInformationRepository)
        {
            _meisterRepository = meisterRepository;
            _contactInformationRepository = contactInformationRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();
            return View(_meisterRepository.GetAllMeisters());
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
                                         Controller = "Meister"
                                     };

            return View("ContactEdit", contactInformation);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            MeisterEditModel meister = _meisterRepository.GetMeisterEditModelById(id);

            return View("Edit", meister);
        }

        [HttpPost]
        public ActionResult Edit(MeisterEditModel modelMeister)
        {
            Meister myMeister = _meisterRepository.GetById(modelMeister.Id);
            _meisterRepository.UpdateMeisterFromMeisterEditModel(modelMeister, myMeister);

            const string title = "Maestro Actualizado";
            var content = "El maestro " + myMeister.FullName + " ha sido actualizado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            Meister meister = _meisterRepository.Delete(id);

            const string title = "Maestro Eliminado";
            var content = "El maestro " + meister.FullName + " ha sido eliminado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ContactAdd(long id)
        {
            var model = new ContactInformationRegisterModel
                        {
                            Id = (int) id,
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
            Meister myMeister = _meisterRepository.GenerateMeisterFromRegisterModel(modelMeister);

            _meisterRepository.Create(myMeister);
            const string title = "Maestro Agregado";
            var content = "El maestro " + myMeister.FullName + "ha sido agregado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            DisplayMeisterModel meister = _meisterRepository.GetMeisterDisplayModelById(id);

            return View("Details", meister);
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            MeisterEditModel meister = _meisterRepository.GetMeisterEditModelById(id);

            return View("DetailsEdit", meister);
        }

        [HttpPost]
        public ActionResult DetailsEdit(MeisterEditModel modelMeister)
        {
            Meister myMeister = _meisterRepository.GetById(modelMeister.Id);
            _meisterRepository.UpdateMeisterFromMeisterEditModel(modelMeister, myMeister);

            const string title = "Maestro Actualizado";
            var content = "El maestro " + myMeister.FullName + " ha sido actualizado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Details/" + modelMeister.Id);
        }
    }
}