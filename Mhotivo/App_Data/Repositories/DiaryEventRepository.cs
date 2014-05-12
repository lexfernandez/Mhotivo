using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
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

    public class AppointmentDiaryRepository : IAppointmentDiaryRepository
    {
        private readonly MhotivoContext _context;

        public AppointmentDiaryRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public AppointmentDiary First(Expression<Func<AppointmentDiary, bool>> query)
        {
            var diaryEvents = _context.AppointmentDiary.First(query);
            return diaryEvents;
        }

        public AppointmentDiary GetById(long id)
        {
            var diaryEvents = _context.AppointmentDiary.Where(x => x.AppointmentDiaryId == id);
            return diaryEvents.Count() != 0 ? diaryEvents.First() : null;
        }

        public AppointmentDiary Create(AppointmentDiary itemToCreate)
        {
            var diaryEvent = _context.AppointmentDiary.Add(itemToCreate);
            return diaryEvent;
        }

        public IQueryable<AppointmentDiary> Query(Expression<Func<AppointmentDiary, AppointmentDiary>> expression)
        {
            return _context.AppointmentDiary.Select(expression);
        }
        
        public IQueryable<AppointmentDiary> Where(Expression<Func<AppointmentDiary, bool>> expression)
        {
            return _context.AppointmentDiary.Where(expression);
        }

        public IQueryable<AppointmentDiary> Filter(Expression<Func<AppointmentDiary, bool>> expression)
        {
            return _context.AppointmentDiary.Where(expression);
        }

        public AppointmentDiary Update(AppointmentDiary itemToUpdate)
        {
            _context.Entry(itemToUpdate).State = EntityState.Modified;
            return itemToUpdate;
        }

        public void Delete(AppointmentDiary itemToDelete)
        {
            _context.AppointmentDiary.Remove(itemToDelete);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}