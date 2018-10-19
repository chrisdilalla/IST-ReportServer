using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess.Code
{
    public interface IDbUtils
    {
        SqlConnection GetSqlConnection(string connStr);
    }

    public class DbUtils: IDbUtils
    {
        public SqlConnection GetSqlConnection(string connStr)
        {
            return GetConnection(connStr);
        }

        public static SqlConnection GetConnection(string connStr)
        {
            SqlConnection conn = new SqlConnection(connStr);
            return conn;
        }

        private static List<string> GetClassPropertiesNames(object aObject, List<string> excludeList = null)
        {
            List<string> propertyList = new List<string>();
            if (aObject != null)
            {
                foreach (var prop in aObject.GetType().GetProperties())
                {
                    bool add = true;
                    if (excludeList != null)
                    {
                        if (excludeList.Any(a => a.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)))
                            add = false;
                    }

                    if (add)
                    {
                        if (!Attribute.IsDefined(prop, typeof(DbIgnored)))
                            propertyList.Add(prop.Name);
                    }
                }
            }
            return propertyList;
        }

        public enum EFieldListType
        {
            Fields,
            Values,
            Update
        }

        /// <summary>
        /// Fields: "Username, FullName, CreatedDate"
        /// Values: "@Username, @FullName, @CreatedDate"
        /// Update: "Username = @Username, FullName = @FullName, CreatedDate = @CreatedDate"
        /// </summary>
        public static string GetDbFieldList(object aObject, EFieldListType listType, List<string> excludeList = null)
        {
            StringBuilder sb = new StringBuilder();

            List<string> allNames = GetClassPropertiesNames(aObject, excludeList);
            foreach (var name in allNames)
            {
                if (sb.Length > 0)
                    sb.Append(", ");

                switch (listType)
                {
                    case EFieldListType.Values:
                        sb.Append("@").Append(name);
                        break;

                    case EFieldListType.Update:
                        sb.Append(name).Append(" = @").Append(name);
                        break;

                    default:
                        sb.Append(name);
                        break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Example: "di.PKey as Docs_PKey, di.CustomerPKey as Docs_CustomerPKey, di.DocName as Docs_DocName, di.ReceivedDate as Docs_ReceivedDate"
        /// </summary>
        /// <param name="aObject"></param>
        /// <param name="alias">SQL sb table/view alias, "di" in the example</param>
        /// <param name="mapperPath">Prefix for splitting, "Docs" in the example</param>
        /// <param name="excludeList">Optional list of fields to exclude from the result</param>
        /// <returns>AutoMapper list of fields</returns>
        public static string GetDbMapperFieldList(object aObject, string alias, string mapperPath, List<string> excludeList = null)
        {
            StringBuilder sb = new StringBuilder();

            List<string> allNames = GetClassPropertiesNames(aObject, excludeList);
            foreach (var name in allNames)
            {
                if (sb.Length > 0)
                    sb.Append(", ");

                sb.Append(alias).Append(".").Append(name).Append(" as ").Append(mapperPath).Append("_").Append(name);
            }

            return sb.ToString();
        }
    }
}

