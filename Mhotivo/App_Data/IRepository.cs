using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Mhotivo.App_Data
{
    public interface IRepository
    {
        Object First(Expression<Func<Object, bool>> query);
        Object GetById(long id);
        Object Create(Object itemToCreate);
        IQueryable<Object> Query(Expression<Func<Object, bool>> expression);
        Object Update(Object itemToUpdate); 
        void Delete(Object itemToDelete);
    }
}