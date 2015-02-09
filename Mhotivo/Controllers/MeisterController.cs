using System.Web.Mvc;
//using Mhotivo.App_Data.Repositories;
//using Mhotivo.App_Data.Repositories.Interfaces;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Implement.Repositories;
using Mhotivo.Data.Entities;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;
using AutoMapper;

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
            //MeisterEditModel meister = _meisterRepository.GetMeisterEditModelById(id);
            var meister = _meisterRepository.GetMeisterEditModelById(id);
            Mapper.CreateMap<MeisterEditModel, Meister>().ReverseMap();
            var meisterModel = Mapper.Map<Meister, MeisterEditModel>(meister);

            return View("Edit", meisterModel);
        }

        [HttpPost]
        public ActionResult Edit(MeisterEditModel modelMeister)
        {
            Meister myMeister = _meisterRepository.GetById(modelMeister.Id);
            Mapper.CreateMap<Meister, MeisterEditModel>().ReverseMap();
            var meisterModel = Mapper.Map<MeisterEditModel, Meister>(modelMeister);

            _meisterRepository.UpdateMeisterFromMeisterEditModel(meisterModel, myMeister);

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
            Mapper.CreateMap<Meister, MeisterRegisterModel>().ReverseMap();
            var meisterModel = Mapper.Map<MeisterRegisterModel, Meister>(modelMeister);

            var myMeister = _meisterRepository.GenerateMeisterFromRegisterModel(meisterModel);

            _meisterRepository.Create(myMeister);
            const string title = "Maestro Agregado";
            var content = "El maestro " + myMeister.FullName + "ha sido agregado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var meister = _meisterRepository.GetMeisterDisplayModelById(id);

            Mapper.CreateMap<DisplayMeisterModel, Meister>().ReverseMap();
            var meisterModel = Mapper.Map<Meister, DisplayMeisterModel>(meister);

            return View("Details", meisterModel);
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            var meister = _meisterRepository.GetMeisterEditModelById(id);

            Mapper.CreateMap<MeisterEditModel, Meister>().ReverseMap();
            var meisterModel = Mapper.Map<Meister, MeisterEditModel>(meister);

            return View("DetailsEdit", meisterModel);
        }

        [HttpPost]
        public ActionResult DetailsEdit(MeisterEditModel modelMeister)
        {
            var myMeister = _meisterRepository.GetById(modelMeister.Id);

            Mapper.CreateMap<Meister, MeisterEditModel>().ReverseMap();
            var meisterModel = Mapper.Map<MeisterEditModel, Meister>(modelMeister);

            _meisterRepository.UpdateMeisterFromMeisterEditModel(meisterModel, myMeister);

            const string title = "Maestro Actualizado";
            var content = "El maestro " + myMeister.FullName + " ha sido actualizado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Details/" + modelMeister.Id);
        }
    }
}