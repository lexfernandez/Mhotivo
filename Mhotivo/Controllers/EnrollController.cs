﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

//using Mhotivo.App_Data.Repositories;
//using Mhotivo.App_Data.Repositories.Interfaces;

using Mhotivo.Interface.Interfaces;
using Mhotivo.Implement.Repositories;
using Mhotivo.Data.Entities;

using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class EnrollController : Controller
    {
        //
        // GET: /Enroll/

        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IEnrollRepository _enrollRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public EnrollController(IPeopleRepository peopleRepository, IAcademicYearRepository academicYearRepository,
            IStudentRepository studentRepository, IEnrollRepository enrollRepository, IGradeRepository gradeRepository)
        {
            _peopleRepository = peopleRepository;
            _studentRepository = studentRepository;
            _enrollRepository = enrollRepository;
            _academicYearRepository = academicYearRepository;
            _gradeRepository = gradeRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();
            return View(_enrollRepository.Query(x => x).ToList()
                .Select(x => new DisplayEnrollStudents
                             {
                                 Id = x.Id,
                                 FullName = x.Student.FullName,
                                 UrlPicture = x.Student.UrlPicture,
                                 Gender = Utilities.GenderToString(x.Student.Gender),
                                 AccountNumber = x.Student.AccountNumber,
                                 Grade = x.AcademicYear.Grade.Name,
                                 Section = x.AcademicYear.Section
                             }));
        }

        [HttpGet]
        public ActionResult Search(string id)
        {
            _viewMessageLogic.SetViewMessageIfExist();
            IEnumerable<DisplayEnrollStudents> model = _enrollRepository.Filter(x => x.Student.FullName.Contains(id))
                .ToList()
                .Select(x => new DisplayEnrollStudents
                             {
                                 Id = x.Id,
                                 FullName = x.Student.FullName,
                                 UrlPicture = x.Student.UrlPicture,
                                 Gender = Utilities.GenderToString(x.Student.Gender),
                                 AccountNumber = x.Student.AccountNumber,
                                 Grade = x.AcademicYear.Grade.Name,
                                 Section = x.AcademicYear.Section
                             });

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            _enrollRepository.Delete(id);

            const string title = "Matricula Borrada";
            const string content = "El estudiante ha sido eliminado exitosamente de la lista de matriculados.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Id = new SelectList(_peopleRepository.Query(x => x), "Id", "FullName");
            ViewBag.GradeId = new SelectList(_gradeRepository.Query(x => x), "GradeId", "Name");
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(EnrollRegisterModel modelEnroll)
        {
            List<AcademicYear> Collection =
                _academicYearRepository.Filter(x => x.Grade.Id == modelEnroll.GradeId).ToList();
            Student student = _studentRepository.GetById(modelEnroll.Id);
            if (Collection.Count > 0)
            {
                foreach (AcademicYear academicYear in Collection)
                {
                    var myEnroll = new Enroll
                                   {
                                       AcademicYear = academicYear,
                                       Student = student
                                   };

                    Enroll enroll = _enrollRepository.Create(myEnroll);
                }
                const string title = "Usuario Agregado";
                const string content = "El usuario ha sido matriculado exitosamente.";
                _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);
            }
            else
            {
                const string title = "Usuario No Agregado";
                const string content = "El usuario no se logro matricular.";
                _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);
            }

            return RedirectToAction("Index");
        }
    }
}