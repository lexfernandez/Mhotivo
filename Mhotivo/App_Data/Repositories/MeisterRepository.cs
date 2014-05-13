using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IMeisterRepository : IDisposable
    {
        Meister First(Expression<Func<Meister, Meister>> query);
        Meister GetById(long id);
        Meister Create(Meister itemToCreate);
        IQueryable<Meister> Query(Expression<Func<Meister, Meister>> expression);
        IQueryable<Meister> Filter(Expression<Func<Meister, bool>> expression);
        Meister Update(Meister itemToUpdate);
        Meister Delete(long id);
        IEnumerable<DisplayMeisterModel> GetAllMeisters();
        Meister GenerateMeisterFromRegisterModel(MeisterRegisterModel meisterRegisterModel);
        MeisterEditModel GetMeisterEditModelById(long id);
        DisplayMeisterModel GetMeisterDisplayModelById(long id);
        Meister UpdateMeisterFromMeisterEditModel(MeisterEditModel meisterEditModel, Meister meister);
        void SaveChanges();
    }

    public class MeisterRepository : IMeisterRepository
    {
        private readonly MhotivoContext _context;

        public MeisterRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public Meister First(Expression<Func<Meister, Meister>> query)
        {
            var meisters = _context.Meisters.Select(query);
            return meisters.Count() != 0 ? meisters.First() : null;
        }

        public Meister GetById(long id)
        {
            var meisters = _context.Meisters.Where(x => x.PeopleId == id);
            return meisters.Count() != 0 ? meisters.First() : null;
        }

        public Meister Create(Meister itemToCreate)
        {
            var meister = _context.Meisters.Add(itemToCreate);
            _context.SaveChanges();
            return meister;
        }

        public IQueryable<Meister> Query(Expression<Func<Meister, Meister>> expression)
        {
            return _context.Meisters.Select(expression);
        }

        public IQueryable<Meister> Filter(Expression<Func<Meister, bool>> expression)
        {
            return _context.Meisters.Where(expression);

        }

        public Meister Update(Meister itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Meister Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Meisters.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public IEnumerable<DisplayMeisterModel> GetAllMeisters()
        {
            return Query(x => x).ToList().Select(x => new DisplayMeisterModel
                {
                    MeisterID = x.PeopleId,
                    IDNumber = x.IDNumber,
                    UrlPicture = x.UrlPicture,
                    FullName = x.FullName,
                    BirthDate = x.BirthDate.ToShortDateString(),
                    Nationality = x.Nationality,
                    Address = x.Address,
                    City = x.City,
                    State = x.State,
                    Country = x.Country,
                    Gender = Utilities.GenderToString(x.Gender),
                    Contacts = x.Contacts,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    StartDate = x.StartDate.ToShortDateString(),
                    EndDate = x.EndDate.ToShortDateString(),
                    Biography = x.Biography

                });
        }

        public DisplayMeisterModel GetMeisterDisplayModelById(long id)
        {
            var meister = GetById(id);
            return new DisplayMeisterModel
            {
                MeisterID = meister.PeopleId,
                IDNumber = meister.IDNumber,
                UrlPicture = meister.UrlPicture,
                FirstName = meister.FirstName,
                LastName = meister.LastName,
                FullName = meister.FullName,
                BirthDate = meister.BirthDate.ToShortDateString(),
                Nationality = meister.Nationality,
                Address = meister.Address,
                City = meister.City,
                State = meister.State,
                Country = meister.Country,
                Gender = Utilities.GenderToString(meister.Gender),
                Contacts = meister.Contacts,
                StartDate = meister.StartDate.ToShortDateString(),
                EndDate = meister.EndDate.ToShortDateString(),
                Biography = meister.Biography
            };
        }

        public Meister UpdateMeisterFromMeisterEditModel(MeisterEditModel meisterEditModel, Meister meister)
        {
            meister.FirstName = meisterEditModel.FirstName;
            meister.LastName = meisterEditModel.LastName;
            meister.FullName = (meisterEditModel.FirstName + " " + meisterEditModel.LastName).Trim();
            meister.Country = meisterEditModel.Country;
            meister.IDNumber = meisterEditModel.IDNumber;
            meister.BirthDate = DateTime.Parse(meisterEditModel.BirthDate);
            meister.Gender = Utilities.IsMasculino(meisterEditModel.Gender);
            meister.Nationality = meisterEditModel.Nationality;
            meister.State = meisterEditModel.State;
            meister.City = meisterEditModel.City;
            meister.Address = meisterEditModel.Address;
            meister.Biography = meisterEditModel.Biography;
            meister.StartDate = DateTime.Parse(meisterEditModel.StartDate);
            meister.EndDate = DateTime.Parse(meisterEditModel.EndDate);
            return Update(meister);
        }

        public Meister GenerateMeisterFromRegisterModel(MeisterRegisterModel meisterRegisterModel)
        {
            return new Meister
            {
                FirstName = meisterRegisterModel.FirstName,
                LastName = meisterRegisterModel.LastName,
                FullName = (meisterRegisterModel.FirstName + " " + meisterRegisterModel.LastName).Trim(),
                IDNumber = meisterRegisterModel.IDNumber,
                BirthDate = DateTime.Parse(meisterRegisterModel.BirthDate),
                Gender = Utilities.IsMasculino(meisterRegisterModel.Gender),
                Nationality = meisterRegisterModel.Nationality,
                State = meisterRegisterModel.State,
                Country = meisterRegisterModel.Country,
                City = meisterRegisterModel.City,
                Address = meisterRegisterModel.Address,
                Biography = meisterRegisterModel.Biography,
                StartDate = DateTime.Parse(meisterRegisterModel.StartDate),
                EndDate = DateTime.Parse(meisterRegisterModel.EndDate)
            };
        }

        public MeisterEditModel GetMeisterEditModelById(long id)
        {
            var meister = GetById(id);
            return new MeisterEditModel
            {
                FirstName = meister.FirstName,
                LastName = meister.LastName,
                FullName = (meister.FirstName + " " + meister.LastName).Trim(),
                IDNumber = meister.IDNumber,
                BirthDate = meister.BirthDate.ToShortDateString(),
                Gender = Utilities.GenderToString(meister.Gender),
                Nationality = meister.Nationality,
                Country = meister.Country,
                State = meister.State,
                City = meister.City,
                Address = meister.Address,
                Id = meister.PeopleId,
                StartDate = meister.StartDate.ToShortDateString(),
                EndDate = meister.EndDate.ToShortDateString(),
                Biography = meister.Biography
            };
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}