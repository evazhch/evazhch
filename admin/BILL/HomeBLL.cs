﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using IBLL;
using Models;
using IDAL;
namespace BLL
{
   
    public class HomeBLL:IHomeBLL
    {
        [Dependency]
        public IHomeRepository HomeRepository { get; set; }
        public List<SysModule> GetMenuByPersonId(string personId, string moduleId)
        {
            return HomeRepository.GetMenuByPersonId(personId, moduleId);
        }
    }
}
