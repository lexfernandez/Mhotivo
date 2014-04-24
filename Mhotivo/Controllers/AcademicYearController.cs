using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Logic;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class AcademicYearController : Controller
    {
        private readonly AcademicYearRepository _academicYearRepository = AcademicYearRepository.Instance;
        private MeisterRepository _meisterRepository =  MeisterRepository.Instance;

        public ActionResult Management()
        {
            //Nota: Agregar esto al momento de mostrar el nombre del teacher para que se pueda mostrar la vista.
            //<a class="toEdit" data-toggle="modal" role="button" data-target="#EditModal" id="/AcademicYear/SelectNewTeacher/@Html.DisplayFor(modelItem => item.Id)">@Html.DisplayFor(modelItem => item.Meister)</a>

            var elements = new AcademicYearViewManagement
            {
                Elements = _academicYearRepository.Filter(x => x.IsActive).ToList().Select(x => new AcademicYearViewData
                {
                    Approved = x.Approved ? "Active" : "Inactive",
                    Course = x.Course.Name,
                    Grade = x.Grade.Name,
                    Id = x.Id,
                    EndDate = (x.TeacherEndDate == null ? "Sin Maestro Asignado" : x.TeacherEndDate.Value.ToShortDateString()),
                    Limit = x.StudentsLimit,
                    Meister = x.Teacher == null ? "Sin Maestro Asignado" : x.Teacher.FullName,
                    Room = x.Room.IsEmpty() ? "Sin Aula Asignada" : x.Room,
                    Schedule = x.Schedule == null ? "Sin Maestro Asignado" : x.Schedule.Value.ToShortTimeString(),
                    Section = x.Section,
                    StartDate = x.TeacherStartDate == null ? "Sin Maestro Asignado" : x.TeacherStartDate.Value.ToShortDateString(),
                    Year = x.Year.Year
                }),
                CurrentYear = DateTime.Now.Year,
                CanGenerate = true
            };

            return View(elements);
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
            _meisterRepository = MeisterRepository.Instance;
            var meister = _meisterRepository.GetById(teacherId);
            academicYear.Teacher = meister;
            _academicYearRepository.Update(academicYear);
            _academicYearRepository.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ManagementPost()
        {
             AcademicYearLogic.Instance().GenerateSectionForGrades();

            return RedirectToAction("Management");
        }



    }
}
