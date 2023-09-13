using System.Collections.Concurrent;
using Abp.Extensions;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;

namespace FM.Framework.Core.App
{
    public static class AppConfigurations
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> ConfigurationCache;

        static AppConfigurations()
        {
            ConfigurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false)
        {
            string key = path + "#" + environmentName + "#" + addUserSecrets;
            return ConfigurationCache.GetOrAdd(key, (_) => BuildConfiguration(path, environmentName, addUserSecrets));
        }

        //
        // 摘要:
        //     根据环境变量来加载 appsettings.json的内容
        //
        // 参数:
        //   path:
        //
        //   environmentName:
        //
        //   addUserSecrets:
        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            if (!environmentName.IsNullOrWhiteSpace())
            {
                configurationBuilder = configurationBuilder.AddJsonFile("appsettings." + environmentName + ".json", optional: true);
            }

            configurationBuilder = configurationBuilder.AddEnvironmentVariables();
            if (addUserSecrets)
            {
                configurationBuilder.AddUserSecrets(typeof(AppConfigurations).GetAssembly());
            }

            return configurationBuilder.Build();
        }
    }
}
