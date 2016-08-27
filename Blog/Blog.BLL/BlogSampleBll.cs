using System;
using System.Collections.Generic;
using System.Linq;
using Blog.IBLL;
using Blog.IDAL;
using Blog.Models;
using Blog.DAL;
using Microsoft.Practices.Unity;
using Blog.Models.sys;

namespace Blog.BLL
{
    public class BlogSampleBLL:IBlogSampleBIll
    {
        
        blogsContainer db = new blogsContainer();
        [Dependency]
        public IBlogSampleRepository Rep { get; set; }
        public List<BlogSampleModel> GetList(string queryStr)
        {
            IQueryable<BlogSample> queryData = null;
            queryData = Rep.Getlist(db);
            return CreateModelList(ref queryData);
        }
        private List<BlogSampleModel> CreateModelList(ref IQueryable<BlogSample> queryData)
        {


            List<BlogSampleModel> modelList = (from r in queryData
                                              select new BlogSampleModel
                                              {
                                                  Id = r.Id,
                                                  Name = r.Name,
                                                  Notice = r.Notice,
                                                  Brows = r.Brows,
                                                  Recommend = r.Recommend,
                                                  Addr = r.Addr,
                                                  CreateTime = r.CreateTime,

                                              }).ToList();

            return modelList;
        }

        public bool Create(BlogSample entity)
        {
            try
            {
                if (Rep.Create(entity) == 1)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {
                //ExceptionHander.WriteException(ex);
                return false;
            }

        }

        public bool Edit(BlogSample entity)
        {
            try
            {
                if (Rep.Edit(entity) == 1)
                {
                    return true;
                }
                else
                {

                    return false;
                }

            }
            catch (Exception ex)
            {

                //ExceptionHander.WriteException(ex);
                return false;
            }

        }

        public bool Delet(string id)
        {
            try
            {
               if( Rep.Delete(id)==1)
               {
                   return true;
               }
                else
               {
                   return false;
               }
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }

        public BlogSample GetById(string id)
        {
            if(IsExist(id))
            {
                BlogSample entity = Rep.GetById(id);
                return entity;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return Rep.IsExist(id);
        }

        
    }
}
