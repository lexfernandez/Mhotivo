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

        public BenefactorRepository(MhotivoContext ctx)
        {
            _context = ctx;
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
