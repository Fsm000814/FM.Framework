using Dapper;

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
namespace FM.FrameWork.Database
{
    public class FMFrameWorkSqlExecutor : ISqlExecutor
    {
        private readonly FMFrameWorkDapperRepository _dapperRepository;

        public FMFrameWorkSqlExecutor(FMFrameWorkDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public IEnumerable<TAny> Query<TAny>(string query, object parameters = null)
            where TAny : class
        {
            return _dapperRepository.Query<TAny>(query, parameters);
        }

        public async Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null)
            where TAny : class
        {
            return await _dapperRepository.QueryAsync<TAny>(query, parameters);
        }

        public int Execute(string query, object parameters = null)
        {
            return _dapperRepository.Execute(query, parameters);
        }

        public async Task<int> ExecuteAsync(string query, object parameters = null)
        {
            return await _dapperRepository.ExecuteAsync(query, parameters);
        }

        public async Task<IDataReader> ExecuteReaderAsync(string query, object parameters = null)
        {
            var connection = await this.GetConnectionAsync();
            return await connection.ExecuteReaderAsync(query, parameters, await this.GetActiveTransactionAsync(), _dapperRepository.Timeout);
        }

        public IDbConnection GetConnection()
        {
            return this._dapperRepository.GetConnection();
        }

        public async Task<IDbConnection> GetConnectionAsync()
        {
            return await this._dapperRepository.GetConnectionAsync();
        }

        public IDbTransaction GetActiveTransaction()
        {
            return this._dapperRepository.GetActiveTransaction();
        }

        public async Task<IDbTransaction> GetActiveTransactionAsync()
        {
            return await this._dapperRepository.GetActiveTransactionAsync();
        }
    }
}
