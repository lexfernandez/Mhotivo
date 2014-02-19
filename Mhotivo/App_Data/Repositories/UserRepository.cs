﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IUserRepository : IDisposable
    {
        User First(Expression<Func<User, User>> query);
        User GetById(long id);
        User Create(User itemToCreate);
        IQueryable<User> Query(Expression<Func<User, User>> expression);
        IQueryable<User> Filter(Expression<Func<User, bool>> expression);
        User Update(User itemToUpdate);
        void Delete(User itemToDelete);
        void SaveChanges();
    }

    public class UserRepository : IUserRepository
    {
        private readonly MhotivoContext _context;

        public UserRepository(MhotivoContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public User First(Expression<Func<User, User>> query)
        {
            return _context.Users.Select(query).FirstOrDefault();
        }

        public User GetById(long id)
        {
            return _context.Users.First(x => x.UserId == id);
        }

        public User Create(User itemToCreate)
        {
            return _context.Users.Add(itemToCreate);
        }

        public IQueryable<User> Query(Expression<Func<User, User>> expression)
        {
            return _context.Users.Select(expression);
        }

        public IQueryable<User> Filter(Expression<Func<User, bool>> expression)
        {
            return _context.Users.Where(expression);
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}