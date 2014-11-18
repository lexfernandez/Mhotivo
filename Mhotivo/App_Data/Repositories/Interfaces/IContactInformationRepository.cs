using System;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
{
    public interface IContactInformationRepository : IDisposable
    {
        ContactInformation First(Expression<Func<ContactInformation, ContactInformation>> query);
        ContactInformation GetById(long id);
        ContactInformation Create(ContactInformation itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<ContactInformation, TResult>> expression);
        IQueryable<ContactInformation> Filter(Expression<Func<ContactInformation, bool>> expression);
        ContactInformation Update(ContactInformation itemToUpdate);
        ContactInformation Delete(long Id);
        void SaveChanges();
    }
}