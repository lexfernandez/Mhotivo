using System;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
{
    public interface IAppointmentDiaryRepository : IDisposable
    {
        AppointmentDiary First(Expression<Func<AppointmentDiary, bool>> query);
        AppointmentDiary GetById(long id);
        AppointmentDiary Create(AppointmentDiary itemToCreate);
        IQueryable<AppointmentDiary> Query(Expression<Func<AppointmentDiary, AppointmentDiary>> expression);
        IQueryable<AppointmentDiary> Where(Expression<Func<AppointmentDiary, bool>> expression);
        IQueryable<AppointmentDiary> Filter(Expression<Func<AppointmentDiary, bool>> expression);
        AppointmentDiary Update(AppointmentDiary itemToUpdate);
        void Delete(AppointmentDiary itemToDelete);
        void SaveChanges();
    }
}