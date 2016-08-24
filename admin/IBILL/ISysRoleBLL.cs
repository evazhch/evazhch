using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models.Sys;

namespace IBLL
{
    public interface ISysRoleBLL
    {
        List<SysRoleModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, SysRoleModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Edit(ref ValidationErrors errors, SysRoleModel model);
        SysRoleModel GetById(string id);
        bool IsExist(string id);
    }

    
}
