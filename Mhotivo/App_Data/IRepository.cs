using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Mhotivo.App_Data
{
    public interface IRepository
    {
        T First<T>(Expression<Func<T, bool>> query) where T : class;
        T GetById<T>(long id) where T : class;
        T Create<T>(T itemToCreate) where T : class;
        IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class;
        T Update<T>(T itemToUpdate) where T : class;
        void Delete<T>(T itemToDelete);
    }
}