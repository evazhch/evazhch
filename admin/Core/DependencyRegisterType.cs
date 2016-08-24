using BLL;
using IBLL;
using IDAL;
using DAL;
using Microsoft.Practices.Unity;
namespace Core
{
    public class DependencyRegisterType
    {
        //系统注入
        public static void Container_Sys(ref UnityContainer container)
        {
            container.RegisterType<ISysSampleBLL, SysSampleBLL>();//样例
            container.RegisterType<ISysSampleRepository, SysSampleRepository>();

            //home
            container.RegisterType<IHomeBLL, HomeBLL>();
            container.RegisterType<IHomeRepository, HomeRepository>();
            
            //log
            container.RegisterType<ISysLogBLL, SysLogBLL>();
            container.RegisterType<ISysLogRepository, SysLogRepository>();

            //
            container.RegisterType<ISysExceptionBLL, SysExceptionBLL>();
            container.RegisterType<ISysExceptionRepository, SysExceptionRepository>();

            container.RegisterType<IAccountBLL, AccountBLL>();
            container.RegisterType<IAccountRepository, AccountRepository>();

            container.RegisterType<ISysModuleBLL, SysModuleBLL>();
            container.RegisterType<ISysModuleRepository, SysModuleRepository>();

            container.RegisterType<ISysModuleOperateBLL, SysModuleOperateBLL>();
            container.RegisterType<ISysModuleOperateRepository, SysModuleOperateRepository>();

            container.RegisterType<ISysRightRepository, SysRightRepository>();
            container.RegisterType<ISysUserBLL, SysUserBLL>();

            container.RegisterType<ISysRoleRepository, SysRoleRepository>();
            container.RegisterType<ISysRoleBLL, SysRoleBLL>();
        }
    }
}
