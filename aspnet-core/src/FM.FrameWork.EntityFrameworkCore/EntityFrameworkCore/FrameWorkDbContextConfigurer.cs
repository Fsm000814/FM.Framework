using System;
using System.Data.Common;

using FM.FrameWork.Database;
using FM.FrameWork.EntityFrameworkCore.Extenstions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FM.FrameWork.EntityFrameworkCore
{
    public static class FrameWorkDbContextConfigurer
    {
        public static readonly ILoggerFactory LoggerFactory
            = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

        public static void Configure(IConfiguration configuration, DbContextOptionsBuilder<FrameWorkDbContext> builder,
            string connectionString)
        {
            switch (Database.DatabaseInfo.Instance.DatabaseType)
            {
                case Database.DatabaseTypeEnum.Oracle:
                    builder.UseOracle(connectionString, (config) =>
                    {
                        config.UseOracleSQLCompatibility(OracleSQLCompatibility.V11.ToString());
                        config.MigrationsHistoryTable("__EFMIGRATIONHISTORY");
                    }).AddInterceptors(new CommentCommandInterceptor())
                        //.UseRivenOracleTypeMapping()
                        ;
                    break;

                case Database.DatabaseTypeEnum.MySQL:
                    var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
                    builder.UseMySql(connectionString, serverVersion);
                    break;

                case Database.DatabaseTypeEnum.SqlServer:
                default:
                    builder.UseSqlServer(connectionString, (config) =>
                    {
                    });
                    break;
            }
        }
        public static void Configure(DbContextOptionsBuilder<FrameWorkDbContext> builder, string connectionString)
        {
            switch (Database.DatabaseInfo.Instance.DatabaseType)
            {
                case Database.DatabaseTypeEnum.Oracle:
                    builder.UseOracle(connectionString, (config) =>
                    {
                        config.UseOracleSQLCompatibility(OracleSQLCompatibility.V11.ToString());
                        config.MigrationsHistoryTable("__EFMIGRATIONHISTORY");
                    }).AddInterceptors(new CommentCommandInterceptor());
                    break;

                case Database.DatabaseTypeEnum.MySQL:
                    var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
                    builder.UseMySql(connectionString, serverVersion);
                    break;

                case Database.DatabaseTypeEnum.SqlServer:
                default:
                    builder.UseSqlServer(connectionString, (config) =>
                    {
                    });
                    break;
            }
        }

        public static void Configure(DbContextOptionsBuilder<FrameWorkDbContext> builder, DbConnection connection)
        {
            switch (Database.DatabaseInfo.Instance.DatabaseType)
            {
                case Database.DatabaseTypeEnum.Oracle:
                    builder.UseOracle(connection, (config) =>
                    {
                        config.UseOracleSQLCompatibility(OracleSQLCompatibility.V11.ToString());
                        config.MigrationsHistoryTable("__EFMIGRATIONHISTORY");
                    })
                        //.UseRivenOracleTypeMapping()
                        ;
                    break;

                case Database.DatabaseTypeEnum.MySQL:
                    var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
                    builder.UseMySql(connection, serverVersion);
                    break;

                case Database.DatabaseTypeEnum.SqlServer:
                default:
                    builder.UseSqlServer(connection, (config) =>
                    {
                    });
                    break;
            }
        }

        /// <summary>
        /// 启用 SQL 语句打印日志。
        /// </summary>
        private static void ConfigureLog(DbContextOptionsBuilder<FrameWorkDbContext> builder)
        {
#if DEBUG
            builder.UseLoggerFactory(LoggerFactory);
#endif
        }
    }
}
