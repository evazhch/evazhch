using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Sys;
using Models;

namespace IDAL
{
   public interface ISysLogRepository
    {
        int Create(SysLog entity);
        void Delete(DBContainer db, string[] deleteCollection);
        IQueryable<SysLog> GetList(DBContainer db);
        SysLog GetById(string id);

    }
}
