using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeIBLL
{
   public interface TreeIBLL
    {
        List<EasyTree> GetList(int pid);

        bool Create(EasyTree model);

        bool Delete(int id);

        bool Edit(EasyTree entity);
        bool IsExists(int id);

        EasyTree GetById(int id);
    }
}
