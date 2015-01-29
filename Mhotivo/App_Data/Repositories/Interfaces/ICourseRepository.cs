using System;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
{
    public interface ICourseRepository :  IDisposable
    {
        Course First(Expression<Func<Course, Course>> query);
        Course GetById(long id);
        Course Create(Course itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Course, TResult>> expression);
        IQueryable<Course> Filter(Expression<Func<Course, bool>> expression);
        Course Update(Course itemToUpdate);
        void Delete(Course itemToDelete);
        void SaveChanges();
    }
}