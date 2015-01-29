using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
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
}