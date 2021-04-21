using System;
using System.Web;
using Models;
using System.IO;
using System.Text;
using Common;
using static Models.SysExceptionModel;

namespace App.BLL.Core
{

    /// <summary>
    /// 写入一个异常错误
    /// </summary>
    /// <param name="ex">异常</param>
    public static class ExceptionHander
    {
        /// <summary>
        /// 加入异常日志
        /// </summary>
        /// <param name="ex">异常</param>
        public static void WriteException(Exception ex)
        {

            try
            {
                using (SysExceptionContext db = new SysExceptionContext())
                {
                    SysExceptionModel model = new SysExceptionModel()
                    {
                        Id = ResultHelper.NewId,
                        HelpLink = ex.HelpLink,
                        Message = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        TargetSite = ex.TargetSite.ToString(),
                        Assembly = ex.Data.ToString(),
                        CreateTime = ResultHelper.NowTime

                    };
                    db.sysExceptions.Add(model);
                    db.SaveChanges();
                }
            }
            catch (Exception ep)
            {
                try
                {
                    //异常失败写入txt
                    string path = @"~/exceptionLog.txt";
                    string txtPath = System.Web.HttpContext.Current.Server.MapPath(path);//获取绝对路径
                    using (StreamWriter sw = new StreamWriter(txtPath, true, Encoding.Default))
                    {
                        sw.WriteLine((ex.Message + "|" + ex.StackTrace + "|" + ep.Message + "|" + DateTime.Now.ToString()).ToString());
                        sw.Dispose();
                        sw.Close();
                    }
                    return;
                }
                catch { return; }
            }



        }
    }


}
