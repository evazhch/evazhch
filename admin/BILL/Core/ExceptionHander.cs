using System;
using System.Web.Configuration;
using Models;
using System.IO;
using System.Text;
using Common;

namespace BLL.Core
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
                        using (DBContainer db = new DBContainer())
                        {
                            SysException model = new SysException()
                            {
                                Id = ResultHelper.NewId,
                                HelpLink = ex.HelpLink,
                                Message = ex.Message,
                                Source = ex.Source,
                                StackTrace = ex.StackTrace,
                                TargetSite = ex.TargetSite.ToString(),
                                Data =ex.Data.ToString(),
                                CreateTime = ResultHelper.NowTime
                                
                            };
                            db.SysException.AddObject(model);
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