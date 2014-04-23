using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Logic
{
    public class AcademicYearLogic
    {
        private readonly ICourseRepository _courseRepo = CourseRepository.Instance;
        private readonly IPensumRepository _pensumRepo = PensumRepository.Instance;

        public void GenerateSectionForGrades(IEnumerable<Grade> grades )
        {
            var courses = _courseRepo.Query(x => x);

            foreach (var c in courses)
            {
                var pensums = _pensumRepo.Filter(x => x.Course.CourseId == c.CourseId);
                foreach (var pensum in pensums)
                {
                    
                }
            }
        }
    }
}