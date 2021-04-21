using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentMange.Models;

namespace IBaseDAL
{
    public interface IBaseDAL<T>where T:class
    {
        
        int Create(T entity);

        int Delete(int id);

        int Edit(T entity);

        T GetById(int id);

        bool IsExist(int id);
    }
}
