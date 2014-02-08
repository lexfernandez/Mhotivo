using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public class UserRepository
    {
        private readonly MhotivoContext _context;

        public UserRepository(MhotivoContext context)
        {
            _context = context;
        }

        public User First(Expression<Func<User, bool>> query)
        {
            return _context.Users.FirstOrDefault(query);
        }

        public User GetById(long id)
        {
            return _context.Users.First(x => x.UserId == id);
        }

        public User Create(User itemToCreate)
        {
            return _context.Users.Add(itemToCreate);
        }

        public IQueryable<bool> Query(Expression<Func<User, bool>> expression)
        {
            return _context.Users.Select(expression);
        }

        public User Update(User itemToUpdate)
        {
            _context.Users.Attach(itemToUpdate);
            _context.Entry(itemToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
            return itemToUpdate;
        }

        public void Delete(User itemToDelete)
        {
            _context.Users.Remove(itemToDelete);
        }
    }
}