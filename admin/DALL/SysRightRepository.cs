using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Models;
using Models.Sys;

namespace DAL
{
    public class SysRightRepository : ISysRightRepository, IDisposable
    {
        /// <summary>
        /// 取角色模块的操作权限，用于权限控制
        /// </summary>
        /// <param name="accountid">acount Id</param>
        /// <param name="controller">url</param>
        /// <returns></returns>
        public List<permModel> GetPermission(string accountid, string controller)
        {

            using (DBContainer db = new DBContainer())
            {
                List<permModel> rights = (from r in db.P_Sys_GetRightOperate(accountid, controller)
                                          select new permModel
                                          {
                                              KeyCode = r.KeyCode,
                                              IsValid = r.IsValid
                                          }).ToList();
                return rights;
            }
        }
        public void Dispose()
        {

        }
    }
}
