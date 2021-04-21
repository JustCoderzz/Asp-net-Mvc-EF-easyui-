using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using static Models.SysExceptionModel;

namespace SysExceptionIDAL
{
  public  interface SysExceptionIDAL
    {
        int Create(SysExceptionModel entity);
        IQueryable<SysExceptionModel> GetList(SysExceptionContext db);
        SysExceptionModel GetById(string id);
    }
}
