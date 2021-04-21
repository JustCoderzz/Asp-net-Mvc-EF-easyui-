using Common;
using GridMvc.Pagination;
using StudentMange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIBLL
{
    public interface StudentIBLL
    {
        List<Student> GetList(string queryStr);

        
        bool Create(ref ValidationErrors errors, Student model);

        bool Delete(int id);

        bool Edit(Student entity);
        bool IsExists(int id);

       Student GetById(int id);
    }
}
