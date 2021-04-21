using App.BLL.Core;
using Common;
using StudentDAL;
using StudentIDAL;
using StudentMange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudentMange.Models.Student;

namespace StudentBLL
{
    public class StudentBLL : StudentIBLL.StudentIBLL
    {
        StudentDBContent db = new StudentDBContent();
        IStudentRepository Rep = new StudentRepository();

        public List<Student> GetList(string queryStr)
        {

            IQueryable<Student> queryData = Rep.GetList(db);


            return queryData.ToList();
        }
        public bool Create(ref ValidationErrors errors, Student model)
        {
            try
            {
                Student entity = Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add("主键重复");
                    return false;
                }
                entity = new Student();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.Chinesescore = model.Chinesescore;
                entity.Computerscore = model.Computerscore;
                entity.Englishscore = model.Englishscore;
                entity.Mathscore= model.Mathscore;
                entity.Myclass = model.Myclass;

                if (Rep.Create(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add("插入失败");
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                if (Rep.Delete(id) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception )
            {
                //ExceptionHander.WriteException(ex);
                return false;
            }
        }
        public bool Edit(Student entity)
        {
            try
            {
                if (Rep.Edit(entity) == 1)
                {
                    return true;
                }
                else
                {

                    return false;
                }

            }
            catch (Exception )
            {

                //ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public Student GetById(int id)
        {
            if (IsExist(id))
            {
                Student entity = Rep.GetById(id);


                return entity;
            }
            else
            {
                return null;
            }
        }


        public bool IsExist(int id)
        {
            return Rep.IsExist(id);
        }

        public bool IsExists(int id)
        {
            if (db.Students.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }
    }

}
