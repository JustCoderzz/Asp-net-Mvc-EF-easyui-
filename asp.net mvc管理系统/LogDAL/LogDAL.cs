using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.SysLogModel;

namespace LogDAL
{
   public class LogDAL:LogIDAL.LogIDAL,IDisposable
    {
        public IQueryable<SysLogModel> GetList(LogContext db)
        {
            IQueryable<SysLogModel> list = db.SysLogs.AsQueryable();
            return list;
        }
        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="entity">实体</param>
        public int Create(SysLogModel entity)
        {
            using (LogContext db = new LogContext())
            {
                db.SysLogs.Add(entity);
                return db.SaveChanges();
            }

        }

        /// <summary>
        /// 删除对象集合
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="deleteCollection">集合</param>
        public void Delete(LogContext db, string[] deleteCollection)
        {
            IQueryable<SysLogModel> collection = from f in db.SysLogs
                                            where deleteCollection.Contains(f.Id)
                                            select f;
            foreach (var deleteItem in collection)
            {
                db.SysLogs.Remove(deleteItem);
            }
        }
        /// <summary>
        /// 根据ID获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysLogModel GetById(string id)
        {
            using (LogContext db = new LogContext())
            {
                return db.SysLogs.SingleOrDefault(a => a.Id == id);
            }
        }
        public void Dispose()
        {

        }

        public bool Delete(string id)
        { LogContext log = new LogContext();
            var temp = log.SysLogs.Find(id);
            log.SysLogs.Remove(temp);
            var row = log.SaveChanges();
            if (row > 0)
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
