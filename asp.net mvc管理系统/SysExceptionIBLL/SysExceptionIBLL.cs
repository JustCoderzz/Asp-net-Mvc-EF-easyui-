using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysExceptionIBLL
{
   public interface SysExceptionIBLL
    {
        List<SysExceptionModel> GetList(ref GridPager pager, string queryStr);
        SysExceptionModel GetById(string id);
    }
}
