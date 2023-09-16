using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Database
{
    public class DatabaseInfo
    {
        public DatabaseTypeEnum DatabaseType { get; protected set; }

        protected DatabaseInfo()
        {
            DatabaseType = DatabaseTypeEnum.MySQL;
        }

        protected void LoadFromConfiguraion(IConfiguration configuration)
        {
            var databaseType = configuration.GetValue<string>("ConnectionStrings:DatabaseType")
                ?.ToLower()
                ?.Trim();

            switch (databaseType)
            {
                case "mysql":
                    DatabaseType = DatabaseTypeEnum.MySQL;
                    break;

                case "postgresql":
                    DatabaseType = DatabaseTypeEnum.PostgreSQL;
                    break;

                case "oracle":
                    DatabaseType = DatabaseTypeEnum.Oracle;
                    break;

                case "sqlserver":
                default:
                    DatabaseType = DatabaseTypeEnum.SqlServer;
                    break;
            }
        }

        public static DatabaseInfo Instance { get; } = new DatabaseInfo();

        /// <summary>
        /// 加载数据库配置信息
        /// </summary>
        /// <param name="configuration"></param>
        public static void LoadConfiguraion(IConfiguration configuration)
        {
            DatabaseInfo.Instance.LoadFromConfiguraion(configuration);
        }
    }

    public enum DatabaseTypeEnum
    {
        SqlServer = 0,
        Oracle = 1,
        MySQL = 2,
        PostgreSQL = 3
    }
}
