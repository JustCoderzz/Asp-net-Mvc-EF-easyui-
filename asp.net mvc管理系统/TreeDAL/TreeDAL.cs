using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.EasyTree;

namespace TreeDAL
{
    public class TreeDAL : TreeIDAL.TreeIDAL,IDisposable
    {
        public int Create(EasyTree entity)
        {
            using (TreeContext db = new TreeContext())
            {
                db.Set<EasyTree>().Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            using (TreeContext db = new TreeContext())
            {
               EasyTree entity = db.EasyTrees.SingleOrDefault(a => a.id == id);
                db.Set<EasyTree>().Remove(entity);
                int row = db.SaveChanges();

                return row > 0 ? 1 : 0;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int Edit(EasyTree entity)
        {
            using (TreeContext db = new TreeContext())
            {
                db.Set<EasyTree>().Attach(entity);
                db.Entry<EasyTree>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public EasyTree GetById(int id)
        {
            using (TreeContext db = new TreeContext())
            {
                return db.EasyTrees.SingleOrDefault(a => a.id == id);
            }
        }

        public IQueryable<EasyTree> GetList(TreeContext db)
        {
            IQueryable<EasyTree>
                list = db.EasyTrees.AsQueryable();
            return list;
        }

        

        public bool IsExist(int id)
        {
            using (TreeContext db = new TreeContext())
            {
                EasyTree entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }
    }
}
