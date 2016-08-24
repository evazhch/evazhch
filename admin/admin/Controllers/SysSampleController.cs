using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common;
using IBLL;
using Models.Sys;
using Microsoft.Practices.Unity;
using admin.Controllers;
using admin.core;
using admin.Core;

namespace App.Admin.Controllers
{
    public class SysSampleController : BaseController
    {

        /// <summary>
        /// 业务层注入
        /// </summary>
        [Dependency]
        public ISysSampleBLL m_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        public ActionResult Index()
        {
            return View();
        }

        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysSampleModel> list = m_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysSampleModel()
                        {

                            Id = r.Id,
                            Name = r.Name,
                            Age = r.Age,
                            Bir = r.Bir,
                            Photo = r.Photo,
                            Note = r.Note,
                            CreateTime = r.CreateTime,

                        }).ToArray()

            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #region 创建
        [SupportFilter(ActionName = "Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(SysSampleModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + model.Id + ",Name:" + model.Name, "成功", "创建", "样例程序");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + model.Id + ",Name:" + model.Name + "," + ErrorCol, "失败", "创建", "样例程序");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 修改

        public ActionResult Edit(string id)
        {

            SysSampleModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]

        public JsonResult Edit(SysSampleModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + model.Id + ",Name:" + model.Name, "成功", "修改", "样例程序");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + model.Id + ",Name:" + model.Name + "," + ErrorCol, "失败", "修改", "样例程序");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 详细

        public ActionResult Details(string id)
        {

            SysSampleModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        #endregion

        #region 删除
        [HttpPost]

        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (m_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Ids:" + id, "成功", "删除", "样例程序");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id + "," + ErrorCol, "失败", "删除", "样例程序");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail), JsonRequestBehavior.AllowGet);
            }


        }
        #endregion
        [SupportFilter]
        public ActionResult Test()
        {
            return View();
        }

    }
}
