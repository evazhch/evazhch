using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Models;

namespace DAL
{
    public class AccountRepository:IAccountRepository,IDisposable
    {
        public SysUser Login(string username, string pwd)
        {
            using (DBContainer db = new DBContainer())
            {
                SysUser user = db.SysUser.SingleOrDefault(a => a.UserName == username && a.Password == pwd);
                return user;
            }
        }
        public void Dispose()
        {

        }

    }
}
