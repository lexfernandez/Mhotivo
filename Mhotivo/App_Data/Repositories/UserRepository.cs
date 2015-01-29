﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.App_Data.Repositories.Interfaces;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MhotivoContext _context;

        public UserRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public User First(Expression<Func<User, bool>> query)
        {
            var users = _context.Users.First(query);
            return users;
        }

        public User GetById(long id)
        {
            var users = _context.Users.Where(x => x.Id == id);
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

        public User Update(User itemToUpdate, bool updateRole = true)
        {
            if (updateRole)
                _context.Entry(itemToUpdate.Role).State = EntityState.Modified;
            _context.SaveChanges();
            return itemToUpdate;   
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

        public IEnumerable<DisplayUserModel> GetAllUsers()
        {
            return Query(x => x).ToList().Select(x => new DisplayUserModel
            {
                DisplayName = x.DisplayName,
                Email = x.Email,
                Role = x.Role.Name,
                Status = x.Status ? "Activo" : "Inactivo",
                Id = x.Id
            });
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}