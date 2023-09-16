using Abp.Dependency;
using Abp.Domain.Services;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Database
{
    //
    // 摘要:
    //     SQL 执行器，用于执行一些 SQL 语句。
    public interface ISqlExecutor : IDomainService, ITransientDependency
    {
        //
        // 摘要:
        //     执行一条 SQL 语句并将查询结果返回。
        //
        // 参数:
        //   query:
        //     SQL 语句。
        //
        //   parameters:
        //     SQL 参数。
        //
        // 类型参数:
        //   TAny:
        //     查询的实体类型。
        //
        // 返回结果:
        //     SQL 查询结果。
        IEnumerable<TAny> Query<TAny>(string query, object parameters = null) where TAny : class;

        //
        // 摘要:
        //     执行一条 SQL 语句并将查询结果返回。
        //
        // 参数:
        //   query:
        //     SQL 语句。
        //
        //   parameters:
        //     SQL 参数。
        //
        // 类型参数:
        //   TAny:
        //     查询的实体类型。
        //
        // 返回结果:
        //     SQL 查询结果。
        Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null) where TAny : class;

        //
        // 摘要:
        //     执行一条 SQL 语句并将执行结果数量返回。
        //
        // 参数:
        //   query:
        //     SQL 语句。
        //
        //   parameters:
        //     SQL 参数。
        //
        // 返回结果:
        //     执行结果数量。
        int Execute(string query, object parameters = null);

        //
        // 摘要:
        //     执行一条 SQL 语句并将执行结果数量返回。
        //
        // 参数:
        //   query:
        //     SQL 语句。
        //
        //   parameters:
        //     SQL 参数。
        //
        // 返回结果:
        //     执行结果数量。
        Task<int> ExecuteAsync(string query, object parameters = null);

        //
        // 参数:
        //   query:
        //
        //   parameters:
        Task<IDataReader> ExecuteReaderAsync(string query, object parameters = null);

        //
        // 摘要:
        //     获取数据库链接
        IDbConnection GetConnection();

        //
        // 摘要:
        //     获取数据库链接
        Task<IDbConnection> GetConnectionAsync();

        //
        // 摘要:
        //     获取事务
        IDbTransaction GetActiveTransaction();

        //
        // 摘要:
        //     获取事务
        Task<IDbTransaction> GetActiveTransactionAsync();
    }
}
