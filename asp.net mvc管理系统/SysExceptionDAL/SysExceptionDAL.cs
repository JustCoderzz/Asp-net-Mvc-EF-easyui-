using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.SysExceptionModel;

namespace SysExceptionDAL
{
    public class SysExceptionDAL : SysExceptionIDAL.SysExceptionIDAL,IDisposable
    {
        public int Create(SysExceptionModel entity)
        {
            using (SysExceptionContext db = new SysExceptionContext())
            {
                db.sysExceptions.Add(entity);
                return db.SaveChanges();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public SysExceptionModel GetById(string id)
        {
            using (SysExceptionContext db = new SysExceptionContext())
            {
                return db.sysExceptions.SingleOrDefault(a => a.Id == id);
            }
        }

        public IQueryable<SysExceptionModel> GetList(SysExceptionModel.SysExceptionContext db)
        {
            IQueryable<SysExceptionModel> list = db.sysExceptions.AsQueryable();
            return list;
        }
    }
}
