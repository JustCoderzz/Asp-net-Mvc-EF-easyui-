using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.EasyTree;

namespace TreeBLL
{
    public class TreeBLL : TreeIBLL.TreeIBLL
    {
        private TreeContext db = new TreeContext();
        TreeIDAL.TreeIDAL Rep = new TreeDAL.TreeDAL();
        public List<EasyTree> GetList(int pid)
        {

            IQueryable<EasyTree> queryData = Rep.GetList(db);
            var list= queryData.ToList();
            var result = from l in list
                         where l.pid == pid
                         select l;


            return result.ToList();
        }
        public bool Create(EasyTree entity)
        {
            try
            {
                if (Rep.Create(entity) == 1)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception)
            {
                //ExceptionHander.WriteException(ex);
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                if (Rep.Delete(id) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                //ExceptionHander.WriteException(ex);
                return false;
            }
        }
        public bool Edit(EasyTree entity)
        {
            try
            {
                if (Rep.Edit(entity) == 1)
                {
                    return true;
                }
                else
                {

                    return false;
                }

            }
            catch (Exception)
            {

                //ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public EasyTree GetById(int id)
        {
            if (IsExist(id))
            {
                EasyTree entity = Rep.GetById(id);


                return entity;
            }
            else
            {
                return null;
            }
        }


        public bool IsExist(int id)
        {
            return Rep.IsExist(id);
        }

        public bool IsExists(int id)
        {
            if (db.EasyTrees.SingleOrDefault(a => a.id == id) != null)
            {
                return true;
            }
            return false;
        }

       
    }
}
