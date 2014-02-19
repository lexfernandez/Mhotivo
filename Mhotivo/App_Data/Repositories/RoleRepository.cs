﻿using System;
using System.Data.Entity;
using System.Linq;
using System.ComponentModel;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IRoleRepository :  IDisposable
    {
        Role First(Expression<Func<Role, Role>> query);
        Role GetById(long id);
        Role Create(Role itemToCreate);
        IQueryable<Role> Query(Expression<Func<Role, Role>> expression);
        IQueryable<Role> Filter(Expression<Func<Role, bool>> expression);
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

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Role First(Expression<Func<Role, Role>> query)
        {
            return _context.Roles.Select(query).FirstOrDefault();
        }

        public Role GetById(long id)
        {
            return _context.Roles.First(x => x.RoleId == id);
        }

        public Role Create(Role itemToCreate)
        {
            return _context.Roles.Add(itemToCreate);
            
        }

        public IQueryable<Role> Query(Expression<Func<Role, Role>> expression)
        {
            return _context.Roles.Select(expression);
        }

        public IQueryable<Role> Filter(Expression<Func<Role, bool>> expression)
        {
            return _context.Roles.Where(expression);
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}