using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IStudentRepository
    {
        Student First(Expression<Func<Student, Student>> query);
        Student GetById(long id);
        Student Create(Student itemToCreate);
        IQueryable<Student> Query(Expression<Func<Student, Student>> expression);
        IQueryable<Student> Filter(Expression<Func<Student, bool>> expression);
        Student Update(Student itemToUpdate);
        Student Delete(long id);
        void SaveChanges();
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly MhotivoContext _context;

        private StudentRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public static StudentRepository Instance
        {
            get { return new StudentRepository(new MhotivoContext()); }
        }

        public Student First(Expression<Func<Student, Student>> query)
        {
            var student = _context.Students.Select(query);
            return student.Count() != 0 ? student.Include(x => x.Tutor1).First() : null;
        }

        public Student GetById(long id)
        {
            var student = _context.Students.Where(x => x.PeopleID == id);
            return student.Count() != 0 ? student.Include(x => x.Tutor1).First() : null;
        }

        public Student Create(Student itemToCreate)
        {
            var student = _context.Students.Add(itemToCreate);
            if (itemToCreate.Tutor1 != null)
            {
                _context.Entry(itemToCreate.Tutor1).State = EntityState.Modified;
            }
            if (itemToCreate.Tutor2 != null)
            {
                _context.Entry(itemToCreate.Tutor2).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return student;
        }

        public IQueryable<Student> Query(Expression<Func<Student, Student>> expression)
        {
            var myStudents = _context.Students.Select(expression);
            return myStudents.Count() != 0 ? myStudents.Include(x => x.Tutor1) : myStudents;
        }

        public IQueryable<Student> Filter(Expression<Func<Student, bool>> expression)
        {
            var myStudents = _context.Students.Where(expression);
            return myStudents.Count() != 0 ? myStudents.Include(x => x.Tutor1) : myStudents;
        }

        public Student Update(Student itemToUpdate)
        {
            if (itemToUpdate.Tutor1 != null)
            {
                _context.Entry(itemToUpdate.Tutor1).State = EntityState.Modified;
            }
            if (itemToUpdate.Tutor2 != null)
            {
                _context.Entry(itemToUpdate.Tutor2).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return itemToUpdate;
        }

        public Student UpdateNew(Student itemToUpdate)
        {
            var student = GetById(itemToUpdate.PeopleID);
            student.FirstName = itemToUpdate.FirstName;
            student.LastName = itemToUpdate.LastName;
            student.FullName = itemToUpdate.FullName;
            student.DateOfBirth = itemToUpdate.DateOfBirth;
            student.AccountNumber = itemToUpdate.AccountNumber;
            student.Gender = itemToUpdate.Gender;
            student.Nationality = itemToUpdate.Nationality;
            student.State = itemToUpdate.State;
            student.City = itemToUpdate.City;
            student.StreetAddress = itemToUpdate.StreetAddress;
            student.StartDate = itemToUpdate.StartDate;
            student.BloodType = itemToUpdate.BloodType;
            student.AccountNumber = itemToUpdate.AccountNumber;
            student.Biography = itemToUpdate.Biography;
            student.Tutor1 = itemToUpdate.Tutor1;
            student.Tutor2 = itemToUpdate.Tutor2;

            return Update(student);

        }

        public Student Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.Students.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
