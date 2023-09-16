using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace FM.FrameWork.Localization
{
    public static class FrameWorkLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(FMFrameWorkConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(FrameWorkLocalizationConfigurer).GetAssembly(),
                        "FM.FrameWork.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
