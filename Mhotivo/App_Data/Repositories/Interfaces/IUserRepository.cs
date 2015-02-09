using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        User First(Expression<Func<User, bool>> query);
        User GetById(long id);
        User Create(User itemToCreate);
        IQueryable<User> Query(Expression<Func<User, User>> expression);
        IQueryable<User> Filter(Expression<Func<User, bool>> expression);
        User Update(User itemToUpdate, bool updateRole);
        User Delete(long id);
        void SaveChanges();
        IEnumerable<DisplayUserModel> GetAllUsers();
    }
}