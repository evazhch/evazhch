using System;
using System.Linq;
using Blog.IDAL;
using Blog.Models;
using System.Data;

namespace Blog.DAL
{
    public class BlogSampleRepository:IBlogSampleRepository,IDisposable
    {
        public IQueryable<BlogSample> Getlist(blogsContainer db)
        {
            IQueryable<BlogSample> list = db.BlogSample.AsQueryable();
            return list;
        }
        public int Create(BlogSample entity)
        {
            using (blogsContainer db = new blogsContainer())
            {
                db.BlogSample.AddObject(entity);
                return db.SaveChanges();
            }
        }
        public int Delete(string id)
        {
            using (blogsContainer db= new blogsContainer())
            {
                BlogSample entity =
                db.BlogSample.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.BlogSample.DeleteObject(entity);
                }
                return db.SaveChanges();
            }
        }
        public int Edit(BlogSample entity)
        {
            using (blogsContainer db = new blogsContainer())
            {
                db.BlogSample.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }
        public BlogSample GetById(string id)
        { 
            using (blogsContainer db = new blogsContainer())
            {
                return db.BlogSample.SingleOrDefault(a=>a.Id==id);
            }
        
        }
        public bool IsExist(string id)
        { 
             using (blogsContainer db = new blogsContainer())
            {
                BlogSample entity = GetById(id);
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
