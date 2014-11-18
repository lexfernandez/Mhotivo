using System;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
{
    public interface IGradeRepository
    {
        Grade First(Expression<Func<Grade, bool>> query);
        Grade GetById(long id);
        Grade Create(Grade itemToCreate);
        IQueryable<Grade> Query(Expression<Func<Grade, Grade>> expression);
        IQueryable<Grade> Filter(Expression<Func<Grade, bool>> expression);
        Grade Update(Grade itemToUpdate);
        void Delete(Grade itemToDelete);
        void SaveChanges();
    }
}