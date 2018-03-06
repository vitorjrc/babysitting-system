using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GuguDadah.Authorization;

namespace GuguDadah
{
    [DependsOn(
        typeof(GuguDadahCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class GuguDadahApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<GuguDadahAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(GuguDadahApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
