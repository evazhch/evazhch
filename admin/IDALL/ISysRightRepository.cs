using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Sys;
using Models;

namespace IDAL
{
    public interface ISysRightRepository
    {
        List<permModel> GetPermission(string accountid, string controller);
    }
}
