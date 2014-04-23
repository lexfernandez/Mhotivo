using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;
using Microsoft.Ajax.Utilities;

namespace Mhotivo.Controllers
{
    public class AcademicYearController : Controller
    {
        private AcademicYearRepository _academicYearRepository = AcademicYearRepository.Instance;
        private MeisterRepository _meisterRepository =  MeisterRepository.Instance;
        public ActionResult Management()
        {
            //Nota: Agregar esto al momento de mostrar el nombre del teacher para que se pueda mostrar la vista.
            //<a class="toEdit" data-toggle="modal" role="button" data-target="#EditModal" id="/AcademicYear/SelectNewTeacher/@Html.DisplayFor(modelItem => item.Id)">@Html.DisplayFor(modelItem => item.Meister)</a>


            //solo para probar el codigo
            //var academicYear=new AcademicYearViewManagement();
            //academicYear.CanGenerate = true;
            //academicYear.CurrentYear = 2014;
            //var year = _academicYearRepository.GetById(6);
            //academicYear.Elements = new List<AcademicYearViewData>
            //                        {new AcademicYearViewData
            //                         {
            //                        Approved = year.Approved.ToString(),
            //                        Meister = year.Teacher.ToString(),
            //                        Course = year.Course.ToString(),
            //                        EndDate = year.TeacherEndDate.ToString(),
            //                        Grade = year.Grade.ToString(),
            //                        Id=year.Id,
            //                        Limit = year.StudentsLimit,
            //                        Room = year.Room.ToString(),
            //                        Schedule = year.Schedule.ToString(),
            //                        Section = year.Section,
            //                        StartDate = year.TeacherStartDate.ToString(),
            //                        Year = 2014
            //                         }};
            

            return View();
        }

        [HttpGet]
        public ActionResult SelectNewTeacher(long id)
        {
            var meisters = _meisterRepository.Query(x => x);
            ViewBag.AcademicYearId = id;
            return View(meisters);
        }

        [HttpGet]
        public ActionResult ChangeTeacher(long id,long teacherId)
        {
            var academicYear = _academicYearRepository.GetById(id);
            _meisterRepository=new MeisterRepository(_academicYearRepository.GetContext());
            var meister = _meisterRepository.GetById(teacherId);
            academicYear.Teacher = meister;
            _academicYearRepository.Update(academicYear);
            _academicYearRepository.SaveChanges();
            return RedirectToAction("Index", "Home");
        }



    }
}
