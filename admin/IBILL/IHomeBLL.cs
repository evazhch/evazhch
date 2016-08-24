using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace IBLL
{
    public interface IHomeBLL
    {
        List<SysModule> GetMenuByPersonId(string personId, string moduleId);

    }
}
