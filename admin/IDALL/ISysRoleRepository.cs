using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IDAL
{
    public interface ISysRoleRepository
    {

        IQueryable<SysRole> GetList(DBContainer db);
        int Create(SysRole entity);
        int Delete(string id);
        int Edit(SysRole entity);
        SysRole GetById(string id);
        bool IsExist(string id);
    }

}
