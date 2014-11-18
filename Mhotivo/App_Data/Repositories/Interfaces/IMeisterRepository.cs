using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
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
}