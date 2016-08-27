using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.IDAL
{
    public interface IBlogSampleRepository
    {
        IQueryable<BlogSample> Getlist(blogsContainer db);

        int Create(BlogSample entity);

        int Delete(string id);

        int Edit(BlogSample entity);

        BlogSample GetById(string id);

        bool IsExist(string id);
    }
}
