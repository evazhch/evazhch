using System;
using System.Collections.Generic;
using Common;
using Models;
namespace IBLL
{
    public interface ISysExceptionBLL
    {
        List<SysException> GetList(ref GridPager pager, string queryStr);
        SysException GetById(string id);
    }
}