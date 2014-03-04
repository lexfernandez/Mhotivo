using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{

    public interface IMeisterRepository
    {
        Meister First(Expression<Func<Meister, Meister>> query);
        Meister GetById(long id);
        Meister Create(Meister itemToCreate);
        IQueryable<Meister> Query(Expression<Func<Meister, Meister>> expression);
        IQueryable<Meister> Filter(Expression<Func<Meister, bool>> expression);
        Meister Update(Meister itemToUpdate);
        Meister Delete(long id);
        void SaveChanges();
    }

    public class MeisterRepository : IMeisterRepository
    {
        private readonly MhotivoContext _context;

        private MeisterRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public static MeisterRepository Instance
        {
            get{return new MeisterRepository(new MhotivoContext());}
        }

        public Meister First(Expression<Func<Meister, Meister>> query)
        {
            var meister = _context.Meisters.Select(query);
            return meister.Count() != 0 ? meister.First() : null;
        }

        public Meister GetById(long id)
        {
            var meister = _context.Meisters.Where(x => x.PeopleID == id);
            return meister.Count() != 0 ? meister.First() : null;
        }

        public Meister Create(Meister itemToCreate)
        {
            var meister = _context.Meisters.Add(itemToCreate);
            _context.SaveChanges();
            return meister;
        }

        public IQueryable<Meister>Query(Expression<Func<Meister, Meister>> expression)
        {
            var myMeisters = _context.Meisters.Select(expression);
            return myMeisters;
        }

        public IQueryable<Meister> Filter(Expression<Func<Meister, bool>> expression)
        {
            var myMeisters = _context.Meisters.Where(expression);
            return myMeisters;
        }

        public Meister Update(Meister itemToUpdate)
        {
            //_context.Entry(itemToUpdate.Meisters).State = EntityState.Modified;
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Meister UpdateNew(Meister itemToUpdate)
        {
            var meister = GetById(itemToUpdate.PeopleID);
            meister.FirstName = itemToUpdate.FirstName;
            meister.LastName = itemToUpdate.LastName;
            meister.FullName = itemToUpdate.FullName;
            meister.DateOfBirth = itemToUpdate.DateOfBirth;
            meister.Gender = itemToUpdate.Gender;
            meister.Nationality = itemToUpdate.Nationality;
            meister.State = itemToUpdate.State;
            meister.City = itemToUpdate.City;
            meister.StreetAddress = itemToUpdate.StreetAddress;
            meister.StartDate = itemToUpdate.StartDate;
            meister.EndDate = itemToUpdate.EndDate;
            meister.Biography = itemToUpdate.Biography;
            return Update(meister);

        }

        public Meister Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Meisters.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }


    }

   
}
