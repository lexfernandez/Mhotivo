using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Models;

namespace Mhotivo.App_Data.Repositories
{
    public interface IClassActivityRepository : IDisposable
    {
        ClassActivity First(Expression<Func<ClassActivity, ClassActivity>> query);
        ClassActivity GetById(long id);
        ClassActivity Create(ClassActivity itemToCreate);
        IQueryable<ClassActivity> Query(Expression<Func<ClassActivity, ClassActivity>> expression);
        IQueryable<ClassActivity> Filter(Expression<Func<ClassActivity, bool>> expression);
        ClassActivity Update(ClassActivity itemToUpdate, bool updateRole);
        ClassActivity Delete(long id);
        void SaveChanges();
        AcademicYear GetByIdAY(long id);
    }

    public class ClassActivityRepository : IClassActivityRepository
    {
        private readonly MhotivoContext _context;

        private ClassActivityRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public static ClassActivityRepository Instance
        {
            get { return new ClassActivityRepository(new MhotivoContext()); }
        }

        public ClassActivity First(Expression<Func<ClassActivity, ClassActivity>> query)
        {
            var classactivities = _context.ClassActivities.Select(query);
            return classactivities.Count() != 0 ? classactivities.First() : null;
        }

        public ClassActivity GetById(long id)
        {
            var classactivities = _context.ClassActivities.Where(x => x.Id == id);
            return classactivities.Count() != 0 ? classactivities.First() : null;
        }

        public ClassActivity Create(ClassActivity itemToCreate)
        {
            var classactivity = _context.ClassActivities.Add(itemToCreate);
            _context.Entry(classactivity.AcademicYear).State = EntityState.Modified;
            _context.SaveChanges();
            return classactivity;
        }

        public IQueryable<ClassActivity> Query(Expression<Func<ClassActivity, ClassActivity>> expression)
        {
            return _context.ClassActivities.Select(expression);
            
        }

        public IQueryable<ClassActivity> Filter(Expression<Func<ClassActivity, bool>> expression)
        {
            return _context.ClassActivities.Where(expression);
        }

        public ClassActivity Update(ClassActivity itemToUpdate, bool updateRole)
        {
            throw new NotImplementedException();
        }

        public ClassActivity Update(ClassActivity itemToUpdate)
        {
            _context.Entry(itemToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
            return itemToUpdate;   
        }

        public ClassActivity UpdateNew(ClassActivity itemToUpdate)
        {
            var classactivity = GetById(itemToUpdate.Id);
            classactivity.Name = itemToUpdate.Name;
            classactivity.Description = itemToUpdate.Description;
            classactivity.Type = itemToUpdate.Type;
            classactivity.Value = itemToUpdate.Value;

            return Update(classactivity);
            
        }

        public ClassActivity Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.ClassActivities.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public String ActiveClassActivityDisplaytext(bool active)
        {
            return active ? "Activo" : "Inactivo";
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public AcademicYear GetByIdAY(long id)
        {
            var academicyears = _context.AcademicYears.Where(x => x.Id == id);
            return academicyears.Count() != 0 ? academicyears.First() : null;
        }
    }
}