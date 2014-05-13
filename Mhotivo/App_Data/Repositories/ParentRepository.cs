using System;
using System.Collections.Generic;
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
        IEnumerable<DisplayParentModel> GetAllParents();
        Parent GenerateParentFromRegisterModel(ParentRegisterModel parentRegisterModel);
        ParentEditModel GetParentEditModelById(long id);
        DisplayParentModel GetParentDisplayModelById(long id);
        Parent UpdateParentFromParentEditModel(ParentEditModel parentEditModel, Parent parent);
        void SaveChanges();
    }

    public class ParentRepository : IParentRepository
    {
        private readonly MhotivoContext _context;

        public ParentRepository(MhotivoContext ctx)
        {
            _context = ctx;
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

        public Parent Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Parents.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public IEnumerable<DisplayParentModel> GetAllParents()
        {
            return Query(x => x).ToList().Select(x => new DisplayParentModel
            {
                ParentID = x.PeopleId,
                IDNumber = x.IDNumber,
                UrlPicture = x.UrlPicture,
                FullName = x.FullName,
                BirthDate = x.BirthDate,
                Nationality = x.Nationality,
                Address = x.Address,
                City = x.City,
                State = x.State,
                Country = x.Country,
                Gender = Utilities.GenderToString(x.Gender),
                Contacts = x.Contacts,
                FirstName = x.FirstName,
                LastName = x.LastName
            });
        }

        public DisplayParentModel GetParentDisplayModelById(long id)
        {
            var parent = GetById(id);
            return new DisplayParentModel
            {
                ParentID = parent.PeopleId,
                IDNumber = parent.IDNumber,
                UrlPicture = parent.UrlPicture,
                FirstName = parent.FirstName,
                LastName = parent.LastName,
                FullName = parent.FullName,
                BirthDate = parent.BirthDate,
                Nationality = parent.Nationality,
                Address = parent.Address,
                City = parent.City,
                State = parent.State,
                Country = parent.Country,
                Gender = Utilities.GenderToString(parent.Gender),
                Contacts = parent.Contacts,
            };
        }

        public Parent UpdateParentFromParentEditModel(ParentEditModel parentEditModel, Parent parent)
        {
            parent.FirstName = parentEditModel.FirstName;
            parent.LastName = parentEditModel.LastName;
            parent.FullName = (parentEditModel.FirstName + " " + parentEditModel.LastName).Trim();
            parent.Country = parentEditModel.Country;
            parent.IDNumber = parentEditModel.IDNumber;
            parent.BirthDate = parentEditModel.BirthDate;
            parent.Gender = Utilities.IsMasculino(parentEditModel.Gender);
            parent.Nationality = parentEditModel.Nationality;
            parent.State = parentEditModel.State;
            parent.City = parentEditModel.City;
            parent.Address = parentEditModel.Address;
            return Update(parent);
        }

        public Parent GenerateParentFromRegisterModel(ParentRegisterModel parentRegisterModel)
        {
            return new Parent
            {
                FirstName = parentRegisterModel.FirstName,
                LastName = parentRegisterModel.LastName,
                FullName = (parentRegisterModel.FirstName + " " + parentRegisterModel.LastName).Trim(),
                IDNumber = parentRegisterModel.IDNumber,
                BirthDate = parentRegisterModel.BirthDate,
                Gender = Utilities.IsMasculino(parentRegisterModel.Gender),
                Nationality = parentRegisterModel.Nationality,
                State = parentRegisterModel.State,
                Country = parentRegisterModel.Country,
                City = parentRegisterModel.City,
                Address = parentRegisterModel.Address,
            };
        }

        public ParentEditModel GetParentEditModelById(long id)
        {
            var parent = GetById(id);
            return new ParentEditModel
            {
                FirstName = parent.FirstName,
                LastName = parent.LastName,
                FullName = (parent.FirstName + " " + parent.LastName).Trim(),
                IDNumber = parent.IDNumber,
                BirthDate = parent.BirthDate,
                Gender = Utilities.GenderToString(parent.Gender),
                Nationality = parent.Nationality,
                Country = parent.Country,
                State = parent.State,
                City = parent.City,
                Address = parent.Address,
                Id = parent.PeopleId,
            };
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
