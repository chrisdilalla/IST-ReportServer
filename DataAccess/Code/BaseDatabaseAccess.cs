using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DataAccess.Code
{
    public abstract class BaseDatabaseAccess
    {
        private static volatile string _connectionString;
        private static readonly object ConnectionStringLock;
        private IDbUtils _dbUtils;

        static BaseDatabaseAccess()
        {
            ConnectionStringLock = new object();
        }

        // for compatibility with old code
        protected BaseDatabaseAccess()
        {
            _dbUtils = new DbUtils();
        }

        // depenedency-injected
        protected BaseDatabaseAccess(IDbUtils dbUtils)
        {
            if (dbUtils == null)
                _dbUtils = new DbUtils();
            else
                _dbUtils = dbUtils;
        }

        protected SqlConnection GetSqlConnection()
        {
            SqlConnection sqlConnection = _dbUtils.GetSqlConnection(_connectionString);
            return sqlConnection;
        }


        public static void SetConnectionString(string connectionString)
        {
            if (_connectionString == null)
            {
                lock (ConnectionStringLock)
                {
                    if (_connectionString == null)
                        _connectionString = connectionString;
                }
            }
        }

        protected IDbUtils GetDbUtils()
        {
            return _dbUtils;
        }


        public virtual IEnumerable<T> Query<T>(string sql, object param = null,
            IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                return sqlConnection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            }
        }

        public virtual SqlMapper.GridReader QueryMultiple(string sql, object param = null,
            IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                return sqlConnection.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
            }
        }

        //
        // Summary:
        //     Execute parameterized SQL
        //
        // Returns:
        //     Number of rows affected
        public virtual int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                return sqlConnection.Execute(sql, param, transaction, commandTimeout, commandType);
            }
        }

        protected IEnumerable<string> GetTableColumnNames(string tableName)
        {
            return Query<string>($"select name from syscolumns where id = object_id('{tableName}')").AsList();
        }

        protected string GetRowSizeSql(string tableName)
        {
            IEnumerable<string> cols = GetTableColumnNames(tableName);
            string rowSizeColumnSql = String.Join("+", cols.Select(col => $"isnull(datalength({col}), 1)"));
            return rowSizeColumnSql;
        }
    }
}
