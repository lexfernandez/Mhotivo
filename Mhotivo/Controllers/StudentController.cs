﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    public class StudentController : Controller
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IParentRepository _parentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public StudentController(IStudentRepository studentRepository, IParentRepository parentRepository,
            IContactInformationRepository contactInformationRepository)
        {
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
            _contactInformationRepository = contactInformationRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();
            var allStudents = _studentRepository.GetAllStudents();

            Mapper.CreateMap<DisplayStudentModel, Student>().ReverseMap();
            var allStudentDisplaysModel = allStudents.Select(Mapper.Map<Student, DisplayStudentModel>).ToList();

            return View(allStudentDisplaysModel);
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
                                         Controller = "Student"
                                     };

            return View("ContactEdit", contactInformation);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var student = _studentRepository.GetStudentEditModelById(id);
            Mapper.CreateMap<StudentEditModel, Student>().ReverseMap();
            var studentModel = Mapper.Map<Student, StudentEditModel>(student);

            ViewBag.Tutor1Id = new SelectList(_parentRepository.Query(x => x), "Id", "FullName",
                studentModel.FirstParent);
            ViewBag.Tutor2Id = new SelectList(_parentRepository.Query(x => x), "Id", "FullName",
                studentModel.SecondParent);

            return View("Edit", studentModel);
        }

        [HttpPost]
        public ActionResult Edit(StudentEditModel modelStudent)
        {
            Student myStudent = _studentRepository.GetById(modelStudent.Id);

            Mapper.CreateMap<Student, StudentEditModel>().ReverseMap();
            var studentModel = Mapper.Map<StudentEditModel, Student>(modelStudent);

            _studentRepository.UpdateStudentFromStudentEditModel(studentModel, myStudent);

            const string title = "Estudiante Actualizado";
            var content = "El estudiante " + myStudent.FullName + " ha sido actualizado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            Student student = _studentRepository.Delete(id);

            const string title = "Estudiante Eliminado";
            var content = "El estudiante " + student.FullName + " ha sido eliminado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ContactAdd(long id)
        {
            var model = new ContactInformationRegisterModel
                        {
                            Id = (int) id,
                            Controller = "Student"
                        };
            return View("ContactAdd", model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Tutor1Id = new SelectList(_parentRepository.Query(x => x), "Id", "FullName");
            ViewBag.Tutor2Id = new SelectList(_parentRepository.Query(x => x), "Id", "FullName");
            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(StudentRegisterModel modelStudent)
        {
            Mapper.CreateMap<Student, StudentRegisterModel>().ReverseMap();
            var studentModel = Mapper.Map<StudentRegisterModel, Student>(modelStudent);

            var myStudent = _studentRepository.GenerateStudentFromRegisterModel(studentModel);
            var student = _studentRepository.Create(myStudent);
            const string title = "Estudiante Agregado";
            var content = "El estudiante " + myStudent.FullName + " ha sido agregado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var student = _studentRepository.GetStudentDisplayModelById(id);
            Mapper.CreateMap<DisplayStudentModel, Student>().ReverseMap();
            var studentModel = Mapper.Map<Student, DisplayStudentModel>(student);

            return View("Details", studentModel);
        }

        [HttpPost]
        public ActionResult Details(DisplayStudentModel modelStudent)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            var student = _studentRepository.GetStudentEditModelById(id);

            Mapper.CreateMap<StudentEditModel, Student>().ReverseMap();
            var studentModel = Mapper.Map<Student, StudentEditModel>(student);

            return View("DetailsEdit", studentModel);
        }

        [HttpPost]
        public ActionResult DetailsEdit(StudentEditModel modelStudent)
        {
            var myStudent = _studentRepository.GetById(modelStudent.Id);

            Mapper.CreateMap<Student, StudentEditModel>().ReverseMap();
            var studentModel = Mapper.Map<StudentEditModel, Student>(modelStudent);

            _studentRepository.UpdateStudentFromStudentEditModel(studentModel, myStudent);

            const string title = "Estudiante Actualizado";
            var content = "El estudiante " + myStudent.FullName + " ha sido actualizado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Details/" + modelStudent.Id);
        }
    }
}