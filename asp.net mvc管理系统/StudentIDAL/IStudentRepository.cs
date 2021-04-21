using StudentMange.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudentMange.Models.Student;

namespace StudentIDAL
{
   public   interface IStudentRepository:IBaseDAL.IBaseDAL<Student>
    {
       IQueryable<Student> GetList(StudentDBContent db);
    }
}
