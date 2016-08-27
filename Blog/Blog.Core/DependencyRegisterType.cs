using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Blog.BLL;
using Blog.IBLL;
using Blog.IDAL;
using Blog.DAL;

namespace Blog.Core
{
    public class DependencyRegisterType
    {
        //系统注入
        public static void Container_Sys(ref UnityContainer container)
        {
            container.RegisterType<IBlogSampleRepository, BlogSampleRepository>();//样例
            container.RegisterType<IBlogSampleBIll,BlogSampleBLL>();
        }
    }
}
