using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using IDAL;

namespace DAL
{
    public class SysExceptionRepository : IDisposable,ISysExceptionRepository
    {
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="db">数据库</param>
        /// <returns>集合</returns>
        public IQueryable<SysException> GetList(DBContainer db)
        {
            IQueryable<SysException> list = db.SysException.AsQueryable();
            return list;
        }
        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="entity">实体</param>
        public int Create(SysException entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.SysException.AddObject(entity);
                return db.SaveChanges();
            }

        }


        /// <summary>
        /// 根据ID获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysException GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysException.SingleOrDefault(a => a.Id == id);
            }
        }
        public void Dispose()
        {

        }
    }
}