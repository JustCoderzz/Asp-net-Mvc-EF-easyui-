using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.SysExceptionModel;

namespace SysExceptionBLL
{
    public class SysExceptionBLL : SysExceptionIBLL.SysExceptionIBLL
    {   public  SysExceptionIDAL.SysExceptionIDAL sysException=new SysExceptionDAL.SysExceptionDAL();
        public List<SysExceptionModel> GetList(ref GridPager pager, string queryStr)
        {
            SysExceptionContext db = new SysExceptionContext();
            List<SysExceptionModel> query = null;
            IQueryable<SysExceptionModel> list = sysException.GetList(db);
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Message.Contains(queryStr));
                pager.totalRows = list.Count();
            }
            else
            {
                pager.totalRows = list.Count();
            }

            if (pager.order == "desc")
            {
                query = list.OrderByDescending(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            }
            else
            {
                query = list.OrderBy(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            }

            return query;
        }
        public SysExceptionModel GetById(string id)
        {
            return sysException.GetById(id);
        }
    }
}
