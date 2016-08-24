using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Sys;
using Common;
using Models;

namespace IBLL
{
    public interface ISysLogBLL
    {
        List<SysLog> GetList(ref GridPager pager, string queryStr);
        SysLog GetById(string id);
    }

}
