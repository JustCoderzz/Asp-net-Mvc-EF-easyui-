using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.EasyTree;

namespace TreeIDAL
{
    public interface TreeIDAL : IBaseDAL.IBaseDAL<EasyTree>
    {
        IQueryable<EasyTree> GetList(TreeContext db);
    }
}
