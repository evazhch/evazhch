using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Core;
using IBLL;
using IDAL;
using Microsoft.Practices.Unity;
using Models.Sys;

namespace BLL
{

    public class SysUserBLL : BaseBLL, ISysUserBLL
    {
        [Dependency]
        public ISysRightRepository sysRightRepository { get; set; }
        public List<permModel> GetPermission(string accountid, string controller)
        {
            return sysRightRepository.GetPermission(accountid, controller);
        }


    }

}
