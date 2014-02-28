using System;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IPeopleRepository : IDisposable
    {
        People First(Expression<Func<People, People>> query);
        People GetById(long id);
        People Create(People itemToCreate);
        IQueryable<People> Query(Expression<Func<People, People>> expression);
        IQueryable<People> Filter(Expression<Func<People, bool>> expression);
        People Update(People itemToUpdate);
        People Delete(long id);
        void SaveChanges();
    }

    public class PeopleRepository : IPeopleRepository
    {
        private readonly MhotivoContext _context;
        private const string MasculinoLabel = "Masculino";
        private const string FemeninoLabel = "Femenino";

        private PeopleRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public static PeopleRepository Instance
        {
            get { return new PeopleRepository(new MhotivoContext()); }
        }

        public People First(Expression<Func<People, People>> query)
        {
            var peoples = _context.Peoples.Select(query);
            return peoples.Count() != 0 ? peoples.First() : null;
        }

        public People GetById(long id)
        {
            var peoples = _context.Peoples.Where(x => x.PeopleId == id);
            return peoples.Count() != 0 ? peoples.First() : null;
        }

        public People Create(People itemToCreate)
        {
            var people = _context.Peoples.Add(itemToCreate);
            _context.SaveChanges();
                return people;
        }

        public IQueryable<People> Query(Expression<Func<People, People>> expression)
        {
            return _context.Peoples.Select(expression);
        }

        public IQueryable<People> Filter(Expression<Func<People, bool>> expression)
        {
            return _context.Peoples.Where(expression);
            
        }

        public People Update(People itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;   
        }

        public People UpdateNew(People itemToUpdate)
        {
            var people = GetById(itemToUpdate.PeopleId);
            people.Address = itemToUpdate.Address;
            people.BirthDate = itemToUpdate.BirthDate;
            people.City = itemToUpdate.City;
            people.FirstName = itemToUpdate.FirstName;
            people.FullName = itemToUpdate.FullName;
            people.Masculino = itemToUpdate.Masculino;
            people.LastName = itemToUpdate.LastName;
            people.Nationality = itemToUpdate.Nationality;
            people.State = itemToUpdate.State;
            people.UrlPicture = itemToUpdate.UrlPicture;

            return Update(people);
            
        }

        public People Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Peoples.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public string SexLabel(bool masculino)
        {
            return masculino ? MasculinoLabel : FemeninoLabel;
        }

        public bool IsMasculino(string sex)
        {
            return sex.Equals(MasculinoLabel);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}