using System;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
{
    public interface IClassActivityRepository : IDisposable
    {
        ClassActivity First(Expression<Func<ClassActivity, ClassActivity>> query);
        ClassActivity GetById(long id);
        ClassActivity Create(ClassActivity itemToCreate);
        IQueryable<ClassActivity> Query(Expression<Func<ClassActivity, ClassActivity>> expression);
        IQueryable<ClassActivity> Filter(Expression<Func<ClassActivity, bool>> expression);
        ClassActivity Update(ClassActivity itemToUpdate);
        ClassActivity Delete(ClassActivity itemToDelete);
        void SaveChanges();
    }
}