using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.SysLogModel;

namespace LogIDAL
{
    public interface LogIDAL
    {
        int Create(SysLogModel entity);
        void Delete(LogContext db, string[] deleteCollection);
        IQueryable<SysLogModel> GetList(LogContext db);
        SysLogModel GetById(string id);
        bool Delete(string id);
    }
}
