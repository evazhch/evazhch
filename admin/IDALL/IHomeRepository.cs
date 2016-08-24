using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace IDAL
{
   public interface IHomeRepository
    {
       List<SysModule> GetMenuByPersonId(string personId, string moduleId);
    }
}
