using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IRoleRepository
    {
        Role First(Expression<Func<Role, Role>> query);
        Role GetById(long id);
        Role Create(Role itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Role, TResult>> expression);
        IQueryable<Role> Filter(Expression<Func<Role, bool>> expression);
        Role Update(Role itemToUpdate);
        void Delete(Role itemToDelete);
        void SaveChanges();
    }

    public class RoleRepository : IRoleRepository
    {
        private readonly MhotivoContext _context;
        private static RoleRepository _instance;

        private RoleRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public static RoleRepository Instance
        {
            get { return _instance ?? (_instance = new RoleRepository(new MhotivoContext())); }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
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

        public Role UpdateNew(Role itemToUpdate)
        {
            var role = GetById(itemToUpdate.RoleId);
            role.Name = itemToUpdate.Name;
            role.Description = itemToUpdate.Description;

            return Update(role);
        }

        public void Delete(Role itemToDelete)
        {
            _context.Roles.Remove(itemToDelete);
        }
    }
}