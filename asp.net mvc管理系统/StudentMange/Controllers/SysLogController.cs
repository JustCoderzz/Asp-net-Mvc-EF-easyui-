using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentMange.Controllers
{
    public class SysLogController : Controller
    {
        LogIBLL.LogIBLL logBLL = new LogBLL.LogBLL();
        // GET: SysLog
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysLogModel> list = logBLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysLogModel()
                        {

                            Id = r.Id,
                            Operator = r.Operator,
                            Message = r.Message,
                            Result = r.Result,
                            Type = r.Type,
                            Module = r.Module,
                            CreateTime = r.CreateTime

                        }).ToArray()

            };

            return Json(json);
        }
        public ActionResult Details(string id)
        {

            SysLogModel entity = logBLL.GetById(id);
            SysLogModel info = new SysLogModel()
            {
                Id = entity.Id,
                Operator = entity.Operator,
                Message = entity.Message,
                Result = entity.Result,
                Type = entity.Type,
                Module = entity.Module,
                CreateTime = entity.CreateTime,
            };
            return View(info);
        }
        public string Delete(string Id)
        {
            string result = "";
            
                if (logBLL.Delete(Id))
                {
                    result = "success";
                }
                else
                {
                    result = "error";
                }
            

            return result;

        }
    }
}