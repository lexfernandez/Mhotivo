using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IEventRepository :  IDisposable
    {
        Event First(Expression<Func<Event, Event>> query);
        Event GetById(long id);
        Event Create(Event itemToCreate);
        IQueryable<Event> Query(Expression<Func<Event, Event>> expression);
        Event Update(Event itemToUpdate);
        void Delete(Event itemToDelete);
        void SaveChanges();
    }

    public class EventRepository : IEventRepository
    {
        private readonly MhotivoContext _context;
        private static EventRepository _event;

        public EventRepository(MhotivoContext context)
        {
            _context = context;
        }

        public static void SetInstance(MhotivoContext ctx)
        {
            _event = new EventRepository(ctx);
        }

        public static EventRepository Instance()
        {
            return _event ?? new EventRepository(new MhotivoContext());
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Event First(Expression<Func<Event, Event>> query)
        {
            return _context.Events.Select(query).FirstOrDefault();
        }

        public Event GetById(long id)
        {
            return _context.Events.First(x => x.EventId == id);
        }

        public Event Create(Event itemToCreate)
        {
            return _context.Events.Add(itemToCreate);
            
        }

        public IQueryable<Event> Query(Expression<Func<Event, Event>> expression)
        {
            return _context.Events.Select(expression);
        }

        public Event Update(Event itemToUpdate)
        {
            _context.Events.Attach(itemToUpdate);
            _context.Entry(itemToUpdate).State = EntityState.Modified;
            return itemToUpdate;
        }

        public void Delete(Event itemToDelete)
        {
            _context.Events.Remove(itemToDelete);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}