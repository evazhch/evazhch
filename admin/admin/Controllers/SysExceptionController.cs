using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Reflection;
using System.Text;
using Common;
using Models;
using IBLL;
using Models.Sys;
using Microsoft.Practices.Unity;

namespace admin.Controllers
{
    public class SysExceptionController : Controller
    {
        //
        // GET: /SysException/
        [Dependency]
        public ISysExceptionBLL exceptionBLL { get; set; }

        public ActionResult Index()
        {

            return View();

        }

        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysException> list = exceptionBLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysException()
                        {
                            Id = r.Id,
                            HelpLink = r.HelpLink,
                            Message = r.Message,
                            Source = r.Source,
                            StackTrace = r.StackTrace,
                            TargetSite = r.TargetSite,
                            Data = r.Data,
                            CreateTime = r.CreateTime
                        }).ToArray()

            };
            return Json(json);
        }


        #region 详细

        public ActionResult Details(string id)
        {

            SysException entity = exceptionBLL.GetById(id);
            SysExceptionModel info = new SysExceptionModel()
            {
                Id = entity.Id,
                HelpLink = entity.HelpLink,
                Message = entity.Message,
                Source = entity.Source,
                StackTrace = entity.StackTrace,
                TargetSite = entity.TargetSite,
                Data = entity.Data,
                CreateTime = entity.CreateTime,
            };
            return View(info);
        }

        #endregion
        public ActionResult Error()
        {

            BaseException ex = new BaseException();
            return View(ex);
        }

    }
}