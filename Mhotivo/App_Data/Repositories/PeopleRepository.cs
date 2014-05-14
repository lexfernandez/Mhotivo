using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;
using System.Data.Entity;

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
        People GeneratePeopleFromRegisterModel(PeopleRegisterModel peopleRegisterModel);
        PeopleEditModel GetPeopleEditModelById(long id);
        DisplayPeopleModel GetPeopleDisplayModelById(long id);
        People UpdatePeopleFromPeopleEditModel(PeopleEditModel peopleEditModel, People people);
        void SaveChanges();
        IEnumerable<DisplayPeopleModel> GetAllPeople();
    }

    public class PeopleRepository : IPeopleRepository
    {
        private readonly MhotivoContext _context;

        public PeopleRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }
        
        public People First(Expression<Func<People, People>> query)
        {
            var peoples = _context.Peoples.Select(query);
            return peoples.Count() != 0 ? peoples.First() : null;
        }

        public People GetById(long id)
        {
            var peoples = _context.Peoples.Where(x => x.Id == id);
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
        
        public void Detach(People people)
        {
            _context.Entry(people).State = EntityState.Detached;
        }

        public People Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Peoples.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public DisplayPeopleModel GetPeopleDisplayModelById(long id)
        {
            var people = GetById(id);
            return new DisplayPeopleModel
            {
                Id = people.Id,
                IdNumber = people.IdNumber,
                UrlPicture = people.UrlPicture,
                FullName = people.FullName,
                BirthDay = people.BirthDate.ToShortDateString(),
                Nationality = people.Nationality,
                Address = people.Address,
                City = people.City,
                State = people.State,
                Sexo = Utilities.GenderToString(people.Gender),
            };
        }

        public People UpdatePeopleFromPeopleEditModel(PeopleEditModel peopleEditModel, People people)
        {
            people.FirstName = peopleEditModel.FirstName;
            people.LastName = peopleEditModel.LastName;
            people.FullName = (peopleEditModel.FirstName + " " + peopleEditModel.LastName).Trim();
            people.Country = peopleEditModel.Country;
            people.IdNumber = peopleEditModel.IdNumber;
            people.BirthDate = DateTime.Parse(peopleEditModel.BirthDay);
            people.Gender = Utilities.IsMasculino(peopleEditModel.Sexo);
            people.Nationality = peopleEditModel.Nationality;
            people.State = peopleEditModel.State;
            people.City = peopleEditModel.City;
            people.Address = peopleEditModel.Address;
            return Update(people);
        }

        public People GeneratePeopleFromRegisterModel(PeopleRegisterModel peopleRegisterModel)
        {
            return new People
            {
                FirstName = peopleRegisterModel.FirstName,
                LastName = peopleRegisterModel.LastName,
                FullName = (peopleRegisterModel.FirstName + " " + peopleRegisterModel.LastName).Trim(),
                IdNumber = peopleRegisterModel.IdNumber,
                BirthDate = DateTime.Parse(peopleRegisterModel.BirthDay),
                Gender = Utilities.IsMasculino(peopleRegisterModel.Sexo),
                Nationality = peopleRegisterModel.Nationality,
                State = peopleRegisterModel.State,
                Country = peopleRegisterModel.Country,
                City = peopleRegisterModel.City,
                Address = peopleRegisterModel.Address,
            };
        }

        public PeopleEditModel GetPeopleEditModelById(long id)
        {
            var people = GetById(id);
            return new PeopleEditModel
            {
                FirstName = people.FirstName,
                LastName = people.LastName,
                FullName = (people.FirstName + " " + people.LastName).Trim(),
                IdNumber = people.IdNumber,
                BirthDay = people.BirthDate.ToShortDateString(),
                Sexo = Utilities.GenderToString(people.Gender),
                Nationality = people.Nationality,
                Country = people.Country,
                State = people.State,
                City = people.City,
                Address = people.Address,
                Id = people.Id,
            };
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<DisplayPeopleModel> GetAllPeople()
        {
            return Query(x => x).ToList().Select(x => new DisplayPeopleModel
            {
                Address = x.Address,
                BirthDay = x.BirthDate.ToShortDateString(),
                Id = x.Id,
                Sexo = Utilities.GenderToString(x.Gender),
                City = x.City,
                Nationality = x.Nationality,
                State = x.State,
                UrlPicture = x.UrlPicture,
                FullName = x.FullName
            });
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}