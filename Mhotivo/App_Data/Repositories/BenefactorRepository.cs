using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IBenefactorRepository
    {
        Benefactor First(Expression<Func<Benefactor, Benefactor>> query);
        Benefactor GetById(long id);
        Benefactor Create(Benefactor itemToCreate);
        IQueryable<Benefactor> Query(Expression<Func<Benefactor, Benefactor>> expression);
        IQueryable<Benefactor> Filter(Expression<Func<Benefactor, bool>> expression);
        Benefactor Update(Benefactor itemToUpdate);
        Benefactor Delete(long id);
        void SaveChanges();
    }

    public class BenefactorRepository : IBenefactorRepository
    {
        private readonly MhotivoContext _context;

        private BenefactorRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        private static BenefactorRepository _benefactor;

        public static void SetInstance(MhotivoContext ctx)
        {
            _benefactor = new BenefactorRepository(ctx);
        }

        public static BenefactorRepository Instance
        {
            get { return _benefactor ?? new BenefactorRepository(new MhotivoContext()); }
        }

        public Benefactor First(Expression<Func<Benefactor, Benefactor>> query)
        {
            var benefactor = _context.Benefactors.Select(query);
            return benefactor.Count() != 0 ? benefactor.First() : null;
        }

        public Benefactor GetById(long id)
        {
            var benefactor = _context.Benefactors.Where(x => x.PeopleId == id);
            return benefactor.Count() != 0 ? benefactor.First() : null;
        }

        public Benefactor Create(Benefactor itemToCreate)
        {
            var benefactor = _context.Benefactors.Add(itemToCreate);
            _context.SaveChanges();
            return benefactor;
        }

        public IQueryable<Benefactor> Query(Expression<Func<Benefactor, Benefactor>> expression)
        {
            var myBenefactors = _context.Benefactors.Select(expression);
            return myBenefactors;
        }

        public IQueryable<Benefactor> Filter(Expression<Func<Benefactor, bool>> expression)
        {
            var myBenefactors = _context.Benefactors.Where(expression);
            return myBenefactors;
        }

        public Benefactor Update(Benefactor itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Benefactor UpdateNew(Benefactor itemToUpdate)
        {
            var benefactor = GetById(itemToUpdate.PeopleId);
            benefactor.FirstName = itemToUpdate.FirstName;
            benefactor.LastName = itemToUpdate.LastName;
            benefactor.FullName = itemToUpdate.FullName;
            benefactor.BirthDate = itemToUpdate.BirthDate;
            benefactor.Gender = itemToUpdate.Gender;
            benefactor.Nationality = itemToUpdate.Nationality;
            benefactor.State = itemToUpdate.State;
            benefactor.City = itemToUpdate.City;
            benefactor.Address = itemToUpdate.Address;
            benefactor.Country = itemToUpdate.Country;

            return Update(benefactor);

        }

        public Benefactor Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Benefactors.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        internal void Detach(Benefactor benefactor)
        {
            _context.Entry(benefactor).State = EntityState.Detached;
        }
    }
}
