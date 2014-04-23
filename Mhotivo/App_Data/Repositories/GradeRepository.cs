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
        IQueryable<Grade> Query(Expression<Func<Grade, Grade>> expression);
        IQueryable<Grade> Filter(Expression<Func<Grade, bool>> expression);
        Grade Update(Grade itemToUpdate);
        Grade Delete(long id);
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

        public Grade First(Expression<Func<Grade,Grade>> query)
        {
           
            var grade = _context.Grades.Select(query);
            
            return grade.Count() != 0 ? grade.First() : null;
        }

       

        public Grade GetById(long id)
        {
            var grade = _context.Grades.Where(x => x.Id == id);
            return grade.Count() != 0 ? grade.First() : null;
        }

        public Grade Create(Grade itemToCreate)
        {
            var grade = _context.Grades.Add(itemToCreate);
            _context.SaveChanges();
            return grade;
        }

        public IQueryable<Grade> Query(Expression<Func<Grade, Grade>> expression)
        {
            var myGrades = _context.Grades.Select(expression);
            return myGrades;
        }

        public IQueryable<Grade> Filter(Expression<Func<Grade, bool>> expression)
        {
            var myGrades = _context.Grades.Where(expression);
            return myGrades;
        }

        public Grade Update(Grade itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Grade UpdateNew(Grade itemToUpdate)
        {
            var grade = GetById(itemToUpdate.Id);
            grade.Name = itemToUpdate.Name;
            grade.EducationLevel = itemToUpdate.EducationLevel;
            return Update(grade);

        }

        public Grade Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Grades.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        internal void Detach(Grade grade)
        {
            _context.Entry(grade).State = EntityState.Detached;
        }
    }
}
