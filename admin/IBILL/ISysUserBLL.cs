using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Sys;
using Common;
using Models;

namespace IBLL
{
    public interface ISysUserBLL
    {
        List<permModel> GetPermission(string accountid, string controller);


    }
}
