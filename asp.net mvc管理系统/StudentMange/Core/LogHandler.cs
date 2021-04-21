using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

using LogIBLL;
using Models;
using static Models.SysLogModel;

namespace Core
{
    public static class LogHandler
    {

        public static LogIBLL.LogIBLL logBLL = new LogBLL.LogBLL();
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="oper">操作人</param>
        /// <param name="mes">操作信息</param>
        /// <param name="result">结果</param>
        /// <param name="type">类型</param>
        /// <param name="module">操作模块</param>
        public static void WriteServiceLog(string oper, string mes, string result, string type, string module)
        {


            SysLogModel entity = new SysLogModel();
            entity.Id = ResultHelper.NewId;
            entity.Operator = oper;
            entity.Message = mes;
            entity.Result = result;
            entity.Type = type;
            entity.Module = module;
            entity.CreateTime = ResultHelper.NowTime;
            using (LogContext logRepository = new LogContext())
            {
                logRepository.SysLogs.Add(entity);
                logRepository.SaveChanges();
            }

        }
    }
}
