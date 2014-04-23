using System;
using System.Collections.Generic;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Logic
{
    public class AcademicYearLogic
    {
        private readonly ICourseRepository _courseRepo = CourseRepository.Instance;
        private readonly IPensumRepository _pensumRepo = PensumRepository.Instance;
        private readonly IAcademicYearRepository _academicYearRepo = AcademicYearRepository.Instance;

        public void GenerateSectionForGrades(IEnumerable<Grade> grades )
        {
            var courses = _courseRepo.Query(x => x);

            foreach (var currentCourse in courses)
            {
                var pensums = _pensumRepo.Filter(x => x.Course.CourseId == currentCourse.CourseId);
                foreach (var pensum in pensums)
                {
                    var academicYear = new AcademicYear
                    {
                        Approved = false,
                        Course = currentCourse,
                        Grade = pensum.Grade,
                        IsActive = true,
                        Room = "",
                        Section = 'A',
                        StudentsCount = 0,
                        StudentsLimit = 25,
                        Year = DateTime.Now
                    };

                    _academicYearRepo.Create(academicYear);
                }
            }
        }
    }
}