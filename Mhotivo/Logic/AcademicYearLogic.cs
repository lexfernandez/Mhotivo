using System;
using System.Linq;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Logic
{
    public class AcademicYearLogic
    {
        private readonly ICourseRepository _courseRepo = CourseRepository.Instance;
        private readonly IPensumRepository _pensumRepo = PensumRepository.Instance;
        private readonly IAcademicYearRepository _academicYearRepo = AcademicYearRepository.Instance;

        private static AcademicYearLogic _academicYear;

        public static AcademicYearLogic Instance()
        {
            _academicYear = _academicYear ?? new AcademicYearLogic();
            return _academicYear;
        }

        public void GenerateSectionForGrades()
        {
            var courses = _courseRepo.Query(x => x).ToList();

            foreach (var currentCourse in courses)
            {
                var pensums = _pensumRepo.Filter(x => x.Course.CourseId == currentCourse.CourseId).ToList();
                foreach (var pensum in pensums)
                {
                    var academicYear = new AcademicYear
                    {
                        Teacher = null,
                        Approved = false,
                        Course = currentCourse,
                        Grade = pensum.Grade,
                        IsActive = true,
                        Room = "",
                        Section = 'A',
                        StudentsCount = 0,
                        StudentsLimit = 25,
                        Year = DateTime.Now,
                        Schedule = null,
                        TeacherEndDate = null,
                        TeacherStartDate = null
                    };
                    _academicYearRepo.Create(academicYear);
                }
            }
        }
    }
}