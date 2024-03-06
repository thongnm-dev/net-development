using Dapper;
using System.Data;

namespace Net.Data.Dapper
{
    internal class DapperContext : IDapperContext
    {
        #region Field
        private readonly IDbConnection _connection;

        #endregion

        #region Ctor
        public DapperContext(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }
        #endregion

        /// <summary>
        /// Execute SQL to Insert, Update, Delete
        /// </summary>
        /// <param name="sql">Native query Or Store procedure</param>
        /// <param name="parameters">Parameters</param>
        /// <param name="cmdType">Command type</param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, DynamicParameters? parameters, QueryType cmdType = QueryType.PROCEDURE)
        {
            return await _connection.ExecuteAsync(sql, parameters, commandType: GetCommandType(cmdType));
        }

        /// <summary>
        /// Get list ressult from SQL  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, DynamicParameters? parameters, QueryType cmdType = QueryType.PROCEDURE)
        {
            return await _connection.QueryAsync<T>(sql, parameters, commandType: GetCommandType(cmdType));
        }

        /// <summary>
        /// Get single object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, DynamicParameters parameters, QueryType cmdType = QueryType.PROCEDURE)
        {
            return await _connection.QuerySingleOrDefaultAsync<T>(sql, parameters, commandType: GetCommandType(cmdType));
        }

        #region Method

        /// <summary>
        /// Get Command type from Query type param
        /// </summary>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        private CommandType GetCommandType(QueryType cmdType)
        {
            switch (cmdType)
            {
                case QueryType.PROCEDURE: return CommandType.StoredProcedure;
                case QueryType.NATIVE: return CommandType.Text;
                default: return CommandType.Text;
            }
        }

        #endregion
    }
}
