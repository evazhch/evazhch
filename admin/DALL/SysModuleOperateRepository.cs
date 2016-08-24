using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Models;

namespace DAL
{
    public class SysModuleOperateRepository : ISysModuleOperateRepository, IDisposable
    {

        public IQueryable<SysModuleOperate> GetList(DBContainer db)
        {
            IQueryable<SysModuleOperate> list = db.SysModuleOperate.AsQueryable();
            return list;
        }

        public int Create(SysModuleOperate entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.SysModuleOperate.AddObject(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysModuleOperate entity = db.SysModuleOperate.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.SysModuleOperate.DeleteObject(entity);
                }
                return db.SaveChanges();
            }
        }

        public SysModuleOperate GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysModuleOperate.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysModuleOperate entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }
        public void Dispose()
        {

        }
    }
}
