using Dapper;

namespace Net.Data.Dapper
{
    public interface IDapperContext
    {
        public Task<int> ExecuteAsync(string sql, DynamicParameters ? parameters, QueryType cmdType = QueryType.PROCEDURE);

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, DynamicParameters? parameters, QueryType cmdType = QueryType.PROCEDURE);

        public Task<T> QuerySingleOrDefaultAsync<T>(string sql, DynamicParameters parameters, QueryType cmdType = QueryType.PROCEDURE);
    }
}
