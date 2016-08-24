using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using IDAL;
using Common;
using Models;
using BLL.Core;
using Microsoft.Practices.Unity;

namespace BLL
{
    public class AccountBLL : BaseBLL,IAccountBLL
    {
        [Dependency]
        public IAccountRepository accountRepository { get; set; }
        public SysUser Login(string username, string pwd)
        {
            return accountRepository.Login(username, pwd);


        }
    } 
}
