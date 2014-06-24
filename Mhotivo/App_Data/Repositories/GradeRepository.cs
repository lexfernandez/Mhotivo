using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IGradeRepository
    {
        Grade First(Expression<Func<Grade, bool>> query);
        Grade GetById(long id);
        Grade Create(Grade itemToCreate);
        IQueryable<Grade> Query(Expression<Func<Grade, Grade>> expression);
        IQueryable<Grade> Filter(Expression<Func<Grade, bool>> expression);
        Grade Update(Grade itemToUpdate);
        void Delete(Grade itemToDelete);
        void SaveChanges();
    }

    public class GradeRepository : IGradeRepository
    {
        private readonly MhotivoContext _context;

        public GradeRepository(MhotivoContext ctx)
        {
            _context = ctx;
           
        }
        
        public Grade First(Expression<Func<Grade, bool>> query)
        {
            return _context.Grades.First(query);
        }

        public Grade GetById(long id)
        {

            return _context.Grades.FirstOrDefault(x => x.Id == id);
        }

        public Grade Create(Grade itemToCreate)
        {
            return _context.Grades.Add(itemToCreate);
        }

        public IQueryable<Grade> Query(Expression<Func<Grade, Grade>> expression)
        {
            return _context.Grades.Select(expression);
        }

        public IQueryable<Grade> Filter(Expression<Func<Grade, bool>> expression)
        {
            return _context.Grades.Where(expression);
        }

        public Grade Update(Grade itemToUpdate)
        {
            _context.Entry(itemToUpdate).State = EntityState.Modified;
            return itemToUpdate;
        }

        public void Delete(Grade itemToDelete)
        {
            _context.Grades.Remove(itemToDelete);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}