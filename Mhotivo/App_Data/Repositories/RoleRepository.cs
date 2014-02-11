using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IRoleRepository
    {
        Role First(Expression<Func<Role, bool>> query);
        Role GetById(long id);
        Role Create(Role itemToCreate);
        IQueryable<bool> Query(Expression<Func<Role, bool>> expression);
        Role Update(Role itemToUpdate);
        void Delete(Role itemToDelete);
        void SaveChanges();
    }

    public class RoleRepository : IRoleRepository
    {
        private readonly MhotivoContext _context;

        public RoleRepository(MhotivoContext context)
        {
            _context = context;
        }

        public Role First(Expression<Func<Role, bool>> query)
        {
            return _context.Roles.FirstOrDefault(query);
        }

        public Role GetById(long id)
        {
            return _context.Roles.First(x => x.RoleId == id);
        }

        public Role Create(Role itemToCreate)
        {
            return _context.Roles.Add(itemToCreate);
            
        }

        public IQueryable<bool> Query(Expression<Func<Role, bool>> expression)
        {
            return _context.Roles.Select(expression);
        }

        public Role Update(Role itemToUpdate)
        {
            _context.Roles.Attach(itemToUpdate);
            _context.Entry(itemToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
            return itemToUpdate;
        }

        public void Delete(Role itemToDelete)
        {
            _context.Roles.Remove(itemToDelete);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}