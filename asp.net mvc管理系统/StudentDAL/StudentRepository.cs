using StudentIDAL;
using StudentMange.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudentMange.Models.Student;


namespace StudentDAL
{
    public class StudentRepository : IStudentRepository, IDisposable
    {
        public int Create(Student entity)
        {
            using (StudentDBContent db = new StudentDBContent())
            {
                db.Set<Student>().Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            using (StudentDBContent db = new StudentDBContent())
            {
                Student entity = db.Students.SingleOrDefault(a => a.Id == id);
                db.Set<Student>().Remove(entity);
                int row = db.SaveChanges();

                return row > 0 ? 1 : 0;
            }
        }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

        public int Edit(Student entity)
        {
            using (StudentDBContent db = new StudentDBContent())
            {
                db.Set<Student>().Attach(entity);
                db.Entry<Student>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public Student GetById(int id)
        {
            using (StudentDBContent db = new StudentDBContent())
            {
                return db.Students.SingleOrDefault(a => a.Id == id);
            }
        }

        public IQueryable<Student> GetList(StudentDBContent db)
            {
                IQueryable<Student>
                    list = db.Students.AsQueryable();
                return list;
            }

        public IQueryable<Student> GetList(DbContext db)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int id)
        {
            using (StudentDBContent db = new StudentDBContent())
            {
                Student entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }


        
    } 
}
