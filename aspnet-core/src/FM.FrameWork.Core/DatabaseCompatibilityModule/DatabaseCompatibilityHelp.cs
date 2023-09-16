using FM.FrameWork.Database;
using FM.FrameWork.DatabaseCompatibilityModule.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.DatabaseCompatibilityModule
{
    public class DatabaseCompatibilityHelper
    {
        public static DatabaseCompatibilityOutPut DatabaseCompatibility(DatabaseCompatibilityInput input)
        {
            var res = new DatabaseCompatibilityOutPut();
            var DatabaseType = Database.DatabaseInfo.Instance.DatabaseType;
            switch (input)
            {
                case 0:
                    res.sortrule = SortByCaseMethod(DatabaseType);
                    return res;
            }
            return null;
        }

        /// <summary>
        /// 根据不同数据库判断是否支持不区分大小写排序
        /// </summary>
        /// <param name="DatabaseType"></param>
        /// <returns></returns>
        protected static string SortByCaseMethod(DatabaseTypeEnum DatabaseType)
        {
            string res = null;
            switch (DatabaseType)
            {
                case Database.DatabaseTypeEnum.MySQL:
                    res = "utf8mb4_general_ci";
                    break;

                case Database.DatabaseTypeEnum.SqlServer:
                    res = "Chinese_PRC_CI_AS";
                    break;

                default:
                    //默认不区分大小写
                    break;
            }
            return res;
        }
    }
}
