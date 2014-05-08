using System;
using System.Data.Entity;
using System.Linq;
using System.ComponentModel;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface ICourseRepository :  IDisposable
    {
        Course First(Expression<Func<Course, Course>> query);
        Course GetById(long id);
        Course Create(Course itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Course, TResult>> expression);
        IQueryable<Course> Filter(Expression<Func<Course, bool>> expression);
        Course Update(Course itemToUpdate);
        void Delete(Course itemToDelete);
        void SaveChanges();
    }

    public class CourseRepository : ICourseRepository
    {
        private readonly MhotivoContext _context;

        public CourseRepository(MhotivoContext ctx)
        {
            _context = ctx;
           
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Course First(Expression<Func<Course, Course>> query)
        {
            var courses = _context.Courses.Select(query);
            return courses.Count() != 0 ? courses.First() : null;
        }

        public Course GetById(long id)
        {
            var courses = _context.Courses.Where(x => x.CourseId == id);
            return courses.Count() != 0 ? courses.First() : null;
        }

        public Course Create(Course itemToCreate)
        {
            var role = _context.Courses.Add(itemToCreate);
            _context.SaveChanges();
            return role;
        }

        public IQueryable<TResult> Query<TResult>(Expression<Func<Course, TResult>> expression)
        {
            return _context.Courses.Select(expression);

        }

        public IQueryable<Course> Filter(Expression<Func<Course, bool>> expression)
        {
            return _context.Courses.Where(expression);
        }

        public Course Update(Course itemToUpdate)
        {
            _context.Entry(itemToUpdate).State = EntityState.Modified;
            SaveChanges();
            return itemToUpdate;
        }

        public void Delete(Course itemToDelete)
        {
            _context.Courses.Remove(itemToDelete);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}