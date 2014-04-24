﻿using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.ComponentModel;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IContactRepository : IDisposable
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

    public class ContactInformationRepository : IContactRepository
    {
        private readonly MhotivoContext _context;
        private static ContactInformationRepository _contactInformation;

        private ContactInformationRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public static void SetInstance(MhotivoContext ctx)
        {
            _contactInformation = new ContactInformationRepository(ctx);
        }

        public static ContactInformationRepository Instance
        {
            get { return _contactInformation ?? new ContactInformationRepository(new MhotivoContext()); }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public ContactInformation First(Expression<Func<ContactInformation, ContactInformation>> query)
        {
            var contactInformations = _context.ContactInformations.Select(query);
            return contactInformations.Count() != 0 ? contactInformations.First() : null;
        }

        public ContactInformation GetById(long id)
        {
            var contactInformations = _context.ContactInformations.Where(x => x.ContactId == id);
            return contactInformations.Count() != 0 ? contactInformations.Include(x => x.People).First() : null;
        }

        public ContactInformation Create(ContactInformation itemToCreate)
        {
            var contactInformation = _context.ContactInformations.Add(itemToCreate);
            _context.Entry(contactInformation.People).State = EntityState.Modified;
            _context.SaveChanges();
            return contactInformation;
        }

        public IQueryable<TResult> Query<TResult>(Expression<Func<ContactInformation, TResult>> expression)
        {
            return _context.ContactInformations.Select(expression);

        }

        public IQueryable<ContactInformation> Filter(Expression<Func<ContactInformation, bool>> expression)
        {
            return _context.ContactInformations.Where(expression);
        }

        public ContactInformation Update(ContactInformation itemToUpdate)
        {
            _context.Entry(itemToUpdate).State = EntityState.Modified;
            SaveChanges();
            return itemToUpdate;
        }

        public ContactInformation UpdateNew(ContactInformation itemToUpdate)
        {
            var contactInformation = GetById(itemToUpdate.ContactId);
            contactInformation.Type = itemToUpdate.Type;
            contactInformation.Value = itemToUpdate.Value;

            return Update(contactInformation);
        }

        public ContactInformation Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.ContactInformations.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}