using Abp;

using FM.FrameWork.Database;

using Hangfire;
using Hangfire.MySql;
using Hangfire.MySql.src;
using Hangfire.Oracle.Core;

namespace FM.FrameWork.Hangfire
{
    public static class FMFrameWorkHangfireStorageExtenions
    {
        /// <summary>
        /// 使用 Hangfire Storage
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IGlobalConfiguration UseMedProHangfireStorage(this IGlobalConfiguration configuration, string connectionString)
        {
            Check.NotNull(configuration, nameof(configuration));
            Check.NotNullOrWhiteSpace(connectionString, nameof(connectionString));

            switch (DatabaseInfo.Instance.DatabaseType)
            {
                case DatabaseTypeEnum.Oracle:
                    configuration.UseOracleStorage(connectionString);
                    break;

                case DatabaseTypeEnum.MySQL:
                    configuration.UseMySqlStorage(connectionString);
                    break;

                case DatabaseTypeEnum.SqlServer:

                default:
                    configuration.UseSqlServerStorage(connectionString);
                    break;
            }

            return configuration;
        }

        /// <summary>
        /// 使用Oracle的Hangfire Storage
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IGlobalConfiguration UseOracleStorage(this IGlobalConfiguration configuration, string connectionString)
        {
            Check.NotNull(configuration, nameof(configuration));
            Check.NotNullOrWhiteSpace(connectionString, nameof(connectionString));

            var userId = connectionString.Split(';')[1].Split('=')[1];
            var storage = new OracleStorage(connectionString, new OracleStorageOptions()
            {
                SchemaName = userId
            });

            configuration.UseStorage(storage);

            return configuration;
        }


        /// <summary>
        /// 使用Mysql的Hangfire Storage
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IGlobalConfiguration UseMySqlStorage(this IGlobalConfiguration configuration, string connectionString)
        {
            Check.NotNull(configuration, nameof(configuration));
            Check.NotNullOrWhiteSpace(connectionString, nameof(connectionString));

            var userId = connectionString.Split(';')[3].Split('=')[1];
            var storage = new MySqlStorage(connectionString, new MySqlStorageOptions()
            {
                //DefaultTablesPrefix = userId
            });

            configuration.UseStorage(storage);

            return configuration;
        }
    }
}
