using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Common;

namespace LogIBLL
{
    public interface LogIBLL
    {
        List<SysLogModel> GetList(ref GridPager pager, string queryStr);
        SysLogModel GetById(string id);
        bool Delete(string id);
    }
}
