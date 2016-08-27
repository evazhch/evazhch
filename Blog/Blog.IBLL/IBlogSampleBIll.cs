using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Models.sys;

namespace Blog.IBLL
{
    public interface IBlogSampleBIll
    {
        List<BlogSampleModel> GetList(String queryStr);

        bool Create(BlogSample model);

        bool Edit(BlogSample model);

        bool Delet(string id); 

        BlogSample GetById(string id);

        bool IsExist(string id);


    }
}
