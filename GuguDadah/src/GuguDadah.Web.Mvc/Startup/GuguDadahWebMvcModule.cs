using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GuguDadah.Configuration;

namespace GuguDadah.Web.Startup
{
    [DependsOn(typeof(GuguDadahWebCoreModule))]
    public class GuguDadahWebMvcModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public GuguDadahWebMvcModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<GuguDadahNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GuguDadahWebMvcModule).GetAssembly());
        }
    }
}
