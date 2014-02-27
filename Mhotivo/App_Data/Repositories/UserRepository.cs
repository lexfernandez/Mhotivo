using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IUserRepository
    {
        User First(Expression<Func<User, User>> query);
        User GetById(long id);
        User Create(User itemToCreate);
        IQueryable<User> Query(Expression<Func<User, User>> expression);
        IQueryable<User> Filter(Expression<Func<User, bool>> expression);
        User Update(User itemToUpdate);
        User Delete(long id);
        void SaveChanges();
    }

    public class UserRepository : IUserRepository
    {
        private readonly MhotivoContext _context;

        private UserRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public static UserRepository Instance
        {
            get { return new UserRepository(new MhotivoContext()); }
        }

        public User First(Expression<Func<User, User>> query)
        {
            var users = _context.Users.Select(query);
            return users.Count() != 0 ? users.Include(x => x.Role).First() : null;
        }

        public User GetById(long id)
        {
            var users = _context.Users.Where(x => x.UserId == id);
            return users.Count() != 0 ? users.Include(x => x.Role).First() : null;
        }

        public User Create(User itemToCreate)
        {
            var user = _context.Users.Add(itemToCreate);
            _context.Entry(user.Role).State = EntityState.Modified;
            _context.SaveChanges();
                return user;
        }

        public IQueryable<User> Query(Expression<Func<User, User>> expression)
        {
            var myUsers = _context.Users.Select(expression);
            return myUsers.Count() != 0 ? myUsers.Include(x => x.Role) : myUsers;
            
        }

        public IQueryable<User> Filter(Expression<Func<User, bool>> expression)
        {
            var myUsers = _context.Users.Where(expression);
            return myUsers.Count() != 0 ? myUsers.Include(x => x.Role) : myUsers;
        }

        public User Update(User itemToUpdate)
        {
            _context.Entry(itemToUpdate.Role).State = EntityState.Modified;
            _context.SaveChanges();
            return itemToUpdate;   
        }

        public User UpdateNew(User itemToUpdate)
        {
            var user = GetById(itemToUpdate.UserId);
            user.DisplayName = itemToUpdate.DisplayName;
            user.Email = itemToUpdate.Email;
            user.Password = itemToUpdate.Password;
            user.Role = itemToUpdate.Role;
            user.Status = itemToUpdate.Status;

            return Update(user);
            
        }

        public User Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Users.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public String ActiveUserDisplaytext(bool active)
        {
            return active ? "Activo" : "Inactivo";
        }
    }
}