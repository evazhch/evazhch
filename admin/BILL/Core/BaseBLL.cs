using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace BLL.Core
{
    public abstract class BaseBLL : IDisposable
    {
        protected DBContainer db = new DBContainer();

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }
    }

}
