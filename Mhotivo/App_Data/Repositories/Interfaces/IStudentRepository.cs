using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories.Interfaces
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
        Student GenerateStudentFromRegisterModel(StudentRegisterModel studentRegisterModel);
        StudentEditModel GetStudentEditModelById(long id);
        DisplayStudentModel GetStudentDisplayModelById(long id);
        Student UpdateStudentFromStudentEditModel(StudentEditModel studentEditModel, Student student);
        void SaveChanges();
        IEnumerable<DisplayStudentModel> GetAllStudents();
    }
}