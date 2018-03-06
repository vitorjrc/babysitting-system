using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GuguDadah.Configuration;

namespace GuguDadah.Web.Host.Startup
{
    [DependsOn(
       typeof(GuguDadahWebCoreModule))]
    public class GuguDadahWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public GuguDadahWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GuguDadahWebHostModule).GetAssembly());
        }
    }
}
