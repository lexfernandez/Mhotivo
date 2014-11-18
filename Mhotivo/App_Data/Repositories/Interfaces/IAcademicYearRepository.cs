using System;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
{
    public interface IAcademicYearRepository : IDisposable
    {
        AcademicYear First(Expression<Func<AcademicYear, AcademicYear>> query);
        AcademicYear GetById(long id);
        AcademicYear Create(AcademicYear itemToCreate);
        IQueryable<AcademicYear> Query(Expression<Func<AcademicYear, AcademicYear>> expression);
        IQueryable<AcademicYear> Filter(Expression<Func<AcademicYear, bool>> expression);
        AcademicYear Update(AcademicYear itemToUpdate, bool updateCourse, bool updateGrade, bool updateTeacher);
        AcademicYear Delete(long id);
        void SaveChanges();
    }
}