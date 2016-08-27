using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.BLL;
using Blog.IBLL;
using Blog.Models;
using Blog.Models.sys;
using Microsoft.Practices.Unity;

namespace Blog.Controllers
{
    public class BlogSampleController : Controller
    {
 
         [Dependency]
        public IBlogSampleBIll m_BLL{get;set;}
        
        // GET: BlogSample
        public ActionResult Index()
        {
            List<BlogSampleModel> list = m_BLL.GetList("");
            return View(list);
        }
        [HttpPost]
        public JsonResult GetList()
        {
            List<BlogSampleModel> list = m_BLL.GetList("");
            var json = new
            {
                total = list.Count,
                rows = (from r in list
                        select new BlogSampleModel()
                        {

                            Id = r.Id,
                            Name = r.Name,
                            Author = r.Author,
                            Brows = r.Brows,
                            Recommend = r.Recommend,
                            Notice = r.Notice,
                            CreateTime = r.CreateTime,

                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}