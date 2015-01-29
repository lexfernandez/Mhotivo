using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
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
        IEnumerable<DisplayBenefactorModel> GettAllBenefactors();
        Benefactor GenerateBenefactorFromRegisterModel(BenefactorRegisterModel benefactorRegisterModel);
        BenefactorEditModel GetBenefactorEditModelById(long id);
        DisplayBenefactorModel GetBenefactorDisplayModelById(long id);
        Benefactor UpdateBenefactorFromBenefactorEditModel(BenefactorEditModel editModel, Benefactor benefactorModel);
        void SaveChanges();
    }
}