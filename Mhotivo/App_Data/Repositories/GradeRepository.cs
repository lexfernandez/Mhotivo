using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IGradeRepository
    {
        Grade First(Expression<Func<Grade, Grade>> query);
        Grade GetById(long id);
        Grade Create(Grade itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Grade, TResult>> expression);
        IQueryable<Grade> Filter(Expression<Func<Grade, bool>> expression);
        Grade Update(Grade itemToUpdate);
        void Delete(Grade itemToDelete);
        void SaveChanges();
    }

    public class GradeRepository : IGradeRepository
    {
        private readonly MhotivoContext _context;

        private GradeRepository(MhotivoContext ctx)
        {
            _context = ctx;
           
        }

        public static GradeRepository Instance
        {
            get { return new GradeRepository(new MhotivoContext()); }
        }

        public Grade First(Expression<Func<Grade, Grade>> query)
        {
            throw new NotImplementedException();
        }

        public Grade GetById(long id)
        {
            var grade = _context.Grades.Where(x => x.GradeId == id);
            return grade.Count() != 0 ? grade.First() : null;
        }

        public Grade Create(Grade itemToCreate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TResult> Query<TResult>(Expression<Func<Grade, TResult>> expression)
        {
            return _context.Grades.Select(expression);
        }

        public IQueryable<Grade> Filter(Expression<Func<Grade, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Grade Update(Grade itemToUpdate)
        {
            throw new NotImplementedException();
        }

        public void Delete(Grade itemToDelete)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}