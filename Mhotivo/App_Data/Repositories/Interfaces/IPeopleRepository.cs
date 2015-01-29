using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
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
}