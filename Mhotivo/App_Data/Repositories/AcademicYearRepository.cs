using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IAcademicYearRepository : IDisposable
    {
        AcademicYear First(Expression<Func<AcademicYear, AcademicYear>> query);
        AcademicYear GetById(long id);
        AcademicYear Create(AcademicYear itemToCreate);
        IQueryable<AcademicYear> Query(Expression<Func<AcademicYear, AcademicYear>> expression);
        IQueryable<AcademicYear> Filter(Expression<Func<AcademicYear, bool>> expression);
        AcademicYear Update(AcademicYear itemToUpdate, bool updateCourse, bool updateGrade, bool updateTeacher);
        AcademicYear Delete(long id);
        void SaveChanges();
    }

    public class AcademicYearRepository : IAcademicYearRepository
    {
        private readonly MhotivoContext _context;

        private AcademicYearRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public MhotivoContext GetContext()
        {
            return _context;
        }

        public static AcademicYearRepository Instance
        {
            get { return new AcademicYearRepository(new MhotivoContext()); }
        }

        public AcademicYear First(Expression<Func<AcademicYear, AcademicYear>> query)
        {
            var ayears = _context.AcademicYears.Select(query);
            return ayears.Count() != 0 ? 
                ayears.Include(x => x.Course)
                      .Include(x=> x.Grade)
                      .Include(x=> x.Teacher)
                      .First() : null;
        }

        public AcademicYear GetById(long id)
        {
            var ayears = _context.AcademicYears.Where(x => x.Id == id);
            return ayears.Count() != 0 ?
                ayears.Include(x => x.Course)
                      .Include(x => x.Grade)
                      .Include(x => x.Teacher)
                      .First() : null;
        }

        public AcademicYear Create(AcademicYear itemToCreate)
        {
            var ayear = _context.AcademicYears.Add(itemToCreate);
            _context.Entry(ayear.Course).State = EntityState.Modified;
            _context.Entry(ayear.Grade).State = EntityState.Modified;
            _context.Entry(ayear.Teacher).State = EntityState.Modified;
            _context.SaveChanges();
            return ayear;
        }

        public IQueryable<AcademicYear> Query(Expression<Func<AcademicYear, AcademicYear>> expression)
        {
            var ayears = _context.AcademicYears.Select(expression);
            return ayears.Count() != 0 ?
                ayears.Include(x => x.Course)
                      .Include(x => x.Grade)
                      .Include(x => x.Teacher) : ayears;
        }

        public IQueryable<AcademicYear> Filter(Expression<Func<AcademicYear, bool>> expression)
        {
            var ayears = _context.AcademicYears.Where(expression);
            return ayears.Count() != 0 ?
                ayears.Include(x => x.Course)
                      .Include(x => x.Grade)
                      .Include(x => x.Teacher) : ayears;
        }

        public AcademicYear Update(AcademicYear itemToUpdate, bool updateCourse = true, bool updateGrade = true, 
            bool updateTeacher = true)
        {
            if (updateCourse)
                _context.Entry(itemToUpdate.Course).State = EntityState.Modified;

            if (updateGrade)
                _context.Entry(itemToUpdate.Grade).State = EntityState.Modified;

            if (updateTeacher)
                _context.Entry(itemToUpdate.Teacher).State = EntityState.Modified;

            _context.SaveChanges();
            return itemToUpdate;   
        }

        public AcademicYear UpdateNew(AcademicYear itemToUpdate)
        {
            var updateCourse = false;
            var updateGrade = false;
            var updateTeacher = false;

            var ayear = GetById(itemToUpdate.Id);
            ayear.Approved = itemToUpdate.Approved;
            ayear.IsActive = itemToUpdate.IsActive;
            ayear.Room = itemToUpdate.Room;
            ayear.Schedule = itemToUpdate.Schedule;
            ayear.Section = itemToUpdate.Section;
            ayear.StudentsLimit = itemToUpdate.StudentsLimit;
            ayear.TeacherEndDate = itemToUpdate.TeacherEndDate;
            ayear.TeacherStartDate = itemToUpdate.TeacherStartDate;
            ayear.Year = itemToUpdate.Year;
            ayear.StudentsCount = itemToUpdate.StudentsCount;

            if (ayear.Course.CourseId != itemToUpdate.Course.CourseId)
            {
                ayear.Course = itemToUpdate.Course;
                updateCourse = true;
            }

            if (ayear.Grade.GradeId != itemToUpdate.Grade.GradeId)
            {
                ayear.Course = itemToUpdate.Course;
                updateGrade = true;
            }

            if (ayear.Teacher.PeopleId != itemToUpdate.Teacher.PeopleId)
            {
                ayear.Teacher = itemToUpdate.Teacher;
                updateTeacher = true;
            }

            return Update(ayear, updateCourse, updateGrade, updateTeacher);   
        }

        public AcademicYear Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.AcademicYears.Remove(itemToDelete);
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

        public void Detach(AcademicYear academicYear)
        {
            _context.Entry(academicYear).State = EntityState.Detached;
        }
    }
}