using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static Models.SysLogModel;


namespace LogBLL
{
    public class LogBLL : LogIBLL.LogIBLL

    {
        LogIDAL.LogIDAL Rep = new LogDAL.LogDAL();
        public List<SysLogModel> GetList(ref GridPager pager, string queryStr)
        {
           LogContext db = new LogContext();
            List<SysLogModel> query = null;
            IQueryable<SysLogModel> list = Rep.GetList(db);
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Message.Contains(queryStr) || a.Module.Contains(queryStr));
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
        public SysLogModel GetById(string id)
        {
            return Rep.GetById(id);
        }
        public bool Delete(string id)
        {
            if (Rep.Delete(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
