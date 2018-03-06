using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace GuguDadah.Localization
{
    public static class GuguDadahLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(GuguDadahConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(GuguDadahLocalizationConfigurer).GetAssembly(),
                        "GuguDadah.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
