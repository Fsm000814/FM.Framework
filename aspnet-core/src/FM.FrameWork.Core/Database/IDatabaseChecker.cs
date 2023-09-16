using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Database
{
    public interface IDatabaseChecker
    {
        //
        // 摘要:
        //     判断数据库是否存在
        //
        // 参数:
        //   connectionString:
        //     数据库连接字符串
        bool Exist(string connectionString);

        //
        // 摘要:
        //     获取当前的数据库上下文实例
        DbContext GetDbContext();
    }
}
