using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IRoleRepository :  IDisposable
    {
        Role First(Expression<Func<Role, Role>> query);
        Role GetById(long id);
        Role Create(Role itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Role, TResult>> expression);
        IQueryable<Role> Filter(Expression<Func<Role, bool>> expression);
        Role Update(Role itemToUpdate);
        void Delete(Role itemToDelete);
        void SaveChanges();
        IEnumerable<Role> GetAllRoles();
    }

    public class RoleRepository : IRoleRepository
    {
        private readonly MhotivoContext _context;

        public RoleRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return Query(x => x);
        }

        public Role First(Expression<Func<Role, Role>> query)
        {
            var roles = _context.Roles.Select(query);
            return roles.Count() != 0 ? roles.First() : null;
        }

        public Role GetById(long id)
        {
            var roles = _context.Roles.Where(x => x.RoleId == id);
            return roles.Count() != 0 ? roles.First() : null;
        }

        public Role Create(Role itemToCreate)
        {
            var role = _context.Roles.Add(itemToCreate);
            _context.SaveChanges();
            return role;
        }

        public IQueryable<TResult> Query<TResult>(Expression<Func<Role, TResult>> expression)
        {
            return _context.Roles.Select(expression);

        }

        public IQueryable<Role> Filter(Expression<Func<Role, bool>> expression)
        {
            return _context.Roles.Where(expression);
        }

        public Role Update(Role itemToUpdate)
        {
            _context.Entry(itemToUpdate).State = EntityState.Modified;
            SaveChanges();
            return itemToUpdate;
        }

        public void Delete(Role itemToDelete)
        {
            _context.Roles.Remove(itemToDelete);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}