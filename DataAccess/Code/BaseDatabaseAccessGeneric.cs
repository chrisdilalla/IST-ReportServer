using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataAccess.Code;

namespace MDDataAccess.DataAccess
{
    public abstract class BaseDatabaseAccessGeneric<T>: BaseDatabaseAccess
    {
        private string _tableName;
        protected string TableName {
            get { return _tableName; }
        }

        private List<string> _updateFieldExclusions;
        private List<string> _insertFieldExclusions;

        protected BaseDatabaseAccessGeneric(string tableName,
            IEnumerable<string> updateFieldExclusions = null,
            IDbUtils dbUtils = null): base(dbUtils)
        {
            _tableName = tableName;
            _updateFieldExclusions = new List<string>();
            _updateFieldExclusions.Add("PKey");
            _updateFieldExclusions.Add("CreationDate");
            if (updateFieldExclusions != null)
                _updateFieldExclusions.AddRange(updateFieldExclusions);
            _insertFieldExclusions = new List<string>();
            _insertFieldExclusions.Add("PKey");
            if (updateFieldExclusions != null)
                _insertFieldExclusions.AddRange(updateFieldExclusions);
        }

        public T Read(int pKey)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                var list = sqlConnection.Query<T>(String.Format("SELECT * FROM {0} WHERE PKey = @PKey", _tableName),
                    new { PKey = pKey });
                return list.FirstOrDefault();
            }
        }

        public virtual List<T> ReadAll(int? customerPkey)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                string queryString = String.Format("SELECT * FROM {0}{1}", _tableName, customerPkey.HasValue ? " where CustomerPkey = @CustomerPkey" : String.Empty);
                return sqlConnection.Query<T>(queryString, new { CustomerPkey = customerPkey }).AsList();
            }
        }

        public virtual int Update(T objectToUpdate)
        {
            string list = DbUtils.GetDbFieldList(objectToUpdate, DbUtils.EFieldListType.Update, _updateFieldExclusions);
            string query = String.Format("UPDATE {0} SET " + list + " WHERE PKey = @PKey", _tableName);
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                return sqlConnection.Execute(query, objectToUpdate);
            }
        }

        public virtual int Insert(T objectToInsert)
        {
            string listFields = DbUtils.GetDbFieldList(objectToInsert, DbUtils.EFieldListType.Fields, _insertFieldExclusions);
            string listValues = DbUtils.GetDbFieldList(objectToInsert, DbUtils.EFieldListType.Values, _insertFieldExclusions);
            string query = String.Format("INSERT into {0}(" + listFields + ") VALUES (" + listValues + ")", _tableName);
            query += ";select @@identity";
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                return sqlConnection.Query<int>(query, objectToInsert).Single();
            }
        }

        public virtual int Delete(int id)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                return sqlConnection.Execute(String.Format("DELETE FROM {0} WHERE PKey = @PKey", _tableName),
                    new { PKey = id });
            }
        }
    }
}
