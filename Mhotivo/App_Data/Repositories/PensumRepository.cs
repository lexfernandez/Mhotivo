using System;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;
using System.Data.Entity;

namespace Mhotivo.App_Data.Repositories
{
    public interface IPensumRepository : IDisposable
    {
        Pensum First(Expression<Func<Pensum, Pensum>> query);
        Pensum GetById(long id);
        Pensum Create(Pensum itemToCreate);
        IQueryable<Pensum> Query(Expression<Func<Pensum, Pensum>> expression);
        IQueryable<Pensum> Filter(Expression<Func<Pensum, bool>> expression);
        Pensum Update(Pensum itemToUpdate, bool courseUpdate, bool gradeUpdate);
        Pensum Delete(long id);
        void SaveChanges();
    }

    public class PensumRepository : IPensumRepository
    {
        private readonly MhotivoContext _context;

        private PensumRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public static PensumRepository Instance
        {
            get { return new PensumRepository(new MhotivoContext()); }
        }

        public Pensum First(Expression<Func<Pensum, Pensum>> query)
        {
            var pensums = _context.Pensums.Select(query);
            return pensums.Count() != 0 ? pensums.First() : null;
        }

        public Pensum GetById(long id)
        {
            var pensums = _context.Pensums.Where(x => x.Id == id);
            return pensums.Count() != 0 ? pensums.First() : null;
        }

        public Pensum Create(Pensum itemToCreate)
        {
            var pensum = _context.Pensums.Add(itemToCreate);
            _context.SaveChanges();
                return pensum;
        }

        public IQueryable<Pensum> Query(Expression<Func<Pensum, Pensum>> expression)
        {
            return _context.Pensums.Select(expression);
        }

        public IQueryable<Pensum> Filter(Expression<Func<Pensum, bool>> expression)
        {
            return _context.Pensums.Where(expression);
            
        }

        public Pensum Update(Pensum itemToUpdate, bool updateCourse = true, bool updateGrade = true)
        {
            if (updateCourse)
                _context.Entry(itemToUpdate.Course).State = EntityState.Modified;

            if (updateGrade)
                _context.Entry(itemToUpdate.Grade).State = EntityState.Modified;

            _context.SaveChanges();
            return itemToUpdate;   
        }

        public Pensum UpdateNew(Pensum itemToUpdate)
        {
            var updateCourse = false;
            var updateGrade = false;
            var pensum = GetById(itemToUpdate.Id);

            if (pensum.Course.CourseId != itemToUpdate.Course.CourseId)
            {
                pensum.Course = itemToUpdate.Course;
                updateCourse = true;
            }

            if (pensum.Grade.Id != itemToUpdate.Grade.Id)
            {
                pensum.Grade = itemToUpdate.Grade;
                updateGrade = true;
            }

            return Update(pensum, updateCourse, updateGrade);
            
        }

        public void Detach(Pensum pensum)
        {
            _context.Entry(pensum).State = EntityState.Detached;
        }

        public Pensum Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Pensums.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
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