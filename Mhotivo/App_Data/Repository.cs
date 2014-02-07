using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Web.Mvc;

namespace Mhotivo.App_Data
{
    public class Repository : IRepository
    {
        private readonly MhotivoContext _context;

        public Repository(MhotivoContext context)
        {
            _context = context;
        }

        public T First<T>(Expression<Func<T, bool>> query) where T : class
        {
           
            throw new NotImplementedException();
        }

        public T GetById<T>(long id) where T : class
        {
            throw new NotImplementedException();
        }

        public T Create<T>(T itemToCreate) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class
        {
            throw new NotImplementedException();
        }

        public T Update<T>(T itemToUpdate) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T itemToDelete)
        {
            throw new NotImplementedException();
        }
    }
}