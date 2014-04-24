using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IParentRepository
    {
        Parent First(Expression<Func<Parent, Parent>> query);
        Parent GetById(long id);
        Parent Create(Parent itemToCreate);
        IQueryable<Parent> Query(Expression<Func<Parent, Parent>> expression);
        IQueryable<Parent> Filter(Expression<Func<Parent, bool>> expression);
        Parent Update(Parent itemToUpdate);
        Parent Delete(long id);
        void SaveChanges();
    }

    public class ParentRepository : IParentRepository
    {
        private readonly MhotivoContext _context;
        private static ParentRepository _parent;

        private ParentRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public static void SetInstance(MhotivoContext ctx)
        {
            _parent = new ParentRepository(ctx);
        }

        public static ParentRepository Instance
        {
            get { return _parent ?? new ParentRepository(new MhotivoContext()); }
        }

        public Parent First(Expression<Func<Parent, Parent>> query)
        {
            var parent = _context.Parents.Select(query);
            return parent.Count() != 0 ? parent.First() : null;
        }

        public Parent GetById(long id)
        {
            var parent = _context.Parents.Where(x => x.PeopleId == id);
            return parent.Count() != 0 ? parent.First() : null;
        }

        public Parent Create(Parent itemToCreate)
        {
            var parent = _context.Parents.Add(itemToCreate);
            _context.SaveChanges();
            return parent;
        }

        public IQueryable<Parent> Query(Expression<Func<Parent, Parent>> expression)
        {
            var myParents = _context.Parents.Select(expression);
            return myParents;
        }

        public IQueryable<Parent> Filter(Expression<Func<Parent, bool>> expression)
        {
            var myParents = _context.Parents.Where(expression);
            return myParents;
        }

        public Parent Update(Parent itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Parent UpdateNew(Parent itemToUpdate)
        {
            var parent = GetById(itemToUpdate.PeopleId);
            parent.FirstName = itemToUpdate.FirstName;
            parent.LastName = itemToUpdate.LastName;
            parent.FullName = itemToUpdate.FullName;
            parent.BirthDate = itemToUpdate.BirthDate;
            parent.Gender = itemToUpdate.Gender;
            parent.Nationality = itemToUpdate.Nationality;
            parent.State = itemToUpdate.State;
            parent.City = itemToUpdate.City;
            parent.Address = itemToUpdate.Address;
            parent.Country = itemToUpdate.Country;

            return Update(parent);

        }

        public Parent Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Parents.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        internal void Detach(Parent parent)
        {
            _context.Entry(parent).State = EntityState.Detached;
        }
    }
}
