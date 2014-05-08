using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IEnrollRepository
    {
        Enroll First(Expression<Func<Enroll, Enroll>> query);
        Enroll GetById(long id);
        Enroll Create(Enroll itemToCreate);
        IQueryable<Enroll> Query(Expression<Func<Enroll, Enroll>> expression);
        IQueryable<Enroll> Filter(Expression<Func<Enroll, bool>> expression);
        Enroll Update(Enroll itemToUpdate, bool academicYear, bool student);
        Enroll Delete(long id);
        void SaveChanges();
    }

    public class EnrollRepository : IEnrollRepository
    {
        private readonly MhotivoContext _context;

        public EnrollRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public Enroll First(Expression<Func<Enroll, Enroll>> query)
        {
            var enroll = _context.Enrolls.Select(query);
            return enroll.Count() != 0 ? enroll.First() : null;
        }

        public Enroll GetById(long id)
        {
            var enroll = _context.Enrolls.Where(x => x.Id == id);
            return enroll.Count() != 0 ? enroll.First() : null;
        }

        public Enroll Create(Enroll itemToCreate)
        {
            var enroll = _context.Enrolls.Add(itemToCreate);
            _context.Entry(enroll.AcademicYear).State = EntityState.Modified;
            _context.Entry(enroll.Student).State = EntityState.Modified;
            _context.SaveChanges();
            return enroll;
        }

        public IQueryable<Enroll> Query(Expression<Func<Enroll, Enroll>> expression)
        {
            return _context.Enrolls.Select(expression);
        }

        public IQueryable<Enroll> Filter(Expression<Func<Enroll, bool>> expression)
        {
            var myEnrolls = _context.Enrolls.Where(expression);
            return myEnrolls.Count() != 0 ? myEnrolls : null;
        }

        public Enroll Update(Enroll itemToUpdate, bool academicYear, bool student)
        {
            if (academicYear)
            {
                _context.Entry(itemToUpdate.AcademicYear).State = EntityState.Modified;
            }
            if (student)
            {
                _context.Entry(itemToUpdate.Student).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Enroll Update(Enroll itemToUpdate)
        {
            var enroll = GetById(itemToUpdate.Id);
            bool academicYear = false;
            bool student = false;

            if (enroll.AcademicYear != itemToUpdate.AcademicYear)
            {
                enroll.AcademicYear = itemToUpdate.AcademicYear;
                academicYear = true;
            }
            if (enroll.Student != itemToUpdate.Student)
            {
                enroll.Student = itemToUpdate.Student;
                student = true;
            }

            return Update(enroll, academicYear, student);

        }

        public Enroll Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Enrolls.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Detach(Enroll enroll)
        {
            _context.Entry(enroll).State = EntityState.Detached;
        }
    }
}
