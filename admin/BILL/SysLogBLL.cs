using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using IDAL;
using Common;
using Models.Sys;
using Models;
using IBLL;
namespace BLL
{
    public class SysLogBLL: ISysLogBLL
    {
        [Dependency]
        public ISysLogRepository logRepository { get; set; }
 
      
        public List<SysLog> GetList(ref GridPager pager, string queryStr)
        {
            DBContainer db = new DBContainer();
            List<SysLog> query = null;
            IQueryable<SysLog> list = logRepository.GetList(db);
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Message.Contains(queryStr) || a.Module.Contains(queryStr));
                pager.totalRows = list.Count();
            }
            else
            {
                pager.totalRows = list.Count();
            }

            if (pager.order == "desc")
            {
                query = list.OrderByDescending(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            }
            else
            {
                query = list.OrderBy(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            }


            return query;
        }
        public SysLog GetById(string id)
        {
            return logRepository.GetById(id);
        }
    }
}
