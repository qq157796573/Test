using HomeworkTwo.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Dal
{
    public class SqlCache<T>
    {
        private static string sqlInsert = null;
        private static string sqlUpdate = null;
        private static string sqlSelectAll = null;
        private static string sqlSelectOne = null;
        private static string sqlDelete = null;
        /// <summary>
        /// 获取新增sql 语句
        /// </summary>
        /// <returns></returns>
        public static string GetSqlInsert()
        {
            if (string.IsNullOrEmpty(sqlInsert))
            {
                Type type = typeof(T);
                List<string> columns = new List<string>();
                List<string> values = new List<string>();
                foreach (PropertyInfo prop in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
                {
                    string columnName = prop.Name;
                    if (prop.IsDefined(typeof(NameAttribute), true))
                    {
                        NameAttribute nameAttribute = prop.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
                        if (nameAttribute != null)
                            columnName = nameAttribute.Name;
                    }
                    columns.Add(columnName);
                    values.Add($"@{columnName}");
                }
                Type attributeType = typeof(NameAttribute);
                NameAttribute nameAttributeTable = type.GetCustomAttribute(attributeType) as NameAttribute;
                string tableName = type.Name;
                if (nameAttributeTable != null)
                    tableName = nameAttributeTable.Name;
                sqlInsert = $@"INSERT INTO [{tableName}]({string.Join(",", columns)} )VALUES({string.Join(",", values)}";
            }
            return sqlInsert;
        }
        /// <summary>
        /// 获取修改sql
        /// </summary>
        /// <returns></returns>
        public static string GetSqlUpdate()
        {
            if (string.IsNullOrEmpty(sqlUpdate))
            {
                Type type = typeof(T);
                Type attributeType = typeof(NameAttribute);
                NameAttribute nameAttributeTable = type.GetCustomAttribute(attributeType) as NameAttribute;
                string tableName = type.Name;
                if (nameAttributeTable != null)
                    tableName = nameAttributeTable.Name;
                List<string> conlumns = new List<string>();
                foreach (PropertyInfo prop in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
                {
                    string columnName = prop.Name;
                    if (prop.IsDefined(typeof(NameAttribute), true))
                    {
                        NameAttribute nameAttributeProp = prop.GetCustomAttribute(typeof(NameAttribute)) as NameAttribute;
                        if (nameAttributeProp != null)
                            columnName = nameAttributeProp.Name;
                    }
                    conlumns.Add($"{columnName}=@{columnName}");
                }
                PropertyInfo basePropertyInfo = type.BaseType.GetProperties().FirstOrDefault();
                string baseColumnName = basePropertyInfo.Name;
                if (basePropertyInfo.IsDefined(typeof(NameAttribute), true))
                {
                    NameAttribute columnNameAttribute = basePropertyInfo.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
                    if (null != columnNameAttribute)
                        baseColumnName = columnNameAttribute.Name;
                }
                sqlUpdate = $@" UPDATE [{tableName}] SET {string.Join(",", conlumns) }
                    WHERE {baseColumnName}=@{baseColumnName}";
            }
            return sqlUpdate;
        }
        /// <summary>
        /// 获取删除sql
        /// </summary>
        /// <returns></returns>
        public static string GetSqlDelete()
        {
            if (string.IsNullOrEmpty(sqlDelete))
            {
                Type type = typeof(T);
                Type attributeType = typeof(NameAttribute);
                NameAttribute nameAttributeTable = type.GetCustomAttribute(attributeType) as NameAttribute;
                string tableName = type.Name;
                if (nameAttributeTable != null)
                    tableName = nameAttributeTable.Name;
                PropertyInfo basePropertyInfo = type.BaseType.GetProperties().FirstOrDefault();
                string baseColumnName = basePropertyInfo.Name;
                if (basePropertyInfo.IsDefined(typeof(NameAttribute), true))
                {
                    NameAttribute columnNameAttribute = basePropertyInfo.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
                    if (null != columnNameAttribute)
                        baseColumnName = columnNameAttribute.Name;
                }
                sqlDelete = $@"DELETE FROM [{type.Name}] WHERE {baseColumnName}=@{baseColumnName}";
            }
            return sqlDelete;
        }
        /// <summary>
        /// 获取所有查询sql
        /// </summary>
        /// <returns></returns>
        public static string GetSqlSelectAll()
        {
            if (string.IsNullOrEmpty(sqlSelectAll))
            {
                Type type = typeof(T);
                Type attributeType = typeof(NameAttribute);
                NameAttribute nameAttributeTable = type.GetCustomAttribute(attributeType) as NameAttribute;
                string tableName = type.Name;
                if (nameAttributeTable != null)
                    tableName = nameAttributeTable.Name;
                List<string> columns = new List<string>();
                foreach (var prop in type.GetProperties())
                {
                    string columanName = prop.Name;
                    if (prop.IsDefined(typeof(NameAttribute), true))
                    {
                        NameAttribute nameAttribute = prop.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
                        if (null != nameAttribute)
                            columanName = nameAttribute.Name;
                    }
                    columns.Add(columanName);
                }
                sqlSelectAll = $@"SELECT {string.Join(",", columns)} FROM [{tableName}]";
            }
            return sqlSelectAll;
        }
        /// <summary>
        /// 获取单个查询  sql
        /// </summary>
        /// <returns></returns>
        public static string GetSqlSelectOne()
        {
            if (string.IsNullOrEmpty(sqlSelectOne))
            {
                Type type = typeof(T);
                Type attributeType = typeof(NameAttribute);
                string name = type.BaseType.GetProperties().FirstOrDefault().Name;
                List<string> columns = new List<string>();
                foreach (var prop in type.GetProperties())
                {
                    string columanName = prop.Name;
                    if(prop.IsDefined(typeof(NameAttribute),true))
                    {
                        NameAttribute nameAttribute = prop.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
                        if (null != nameAttribute)
                            columanName = nameAttribute.Name;
                    }
                    columns.Add(columanName);
                }
                NameAttribute nameAttributeTable = type.GetCustomAttribute(attributeType) as NameAttribute;
                string tableName = type.Name;
                if (nameAttributeTable != null)
                    tableName = nameAttributeTable.Name;
                sqlSelectOne = $@"SELECT {string.Join(",", columns)} FROM [{tableName}]
                WHERE {name}=@{name}";
            }
            return sqlSelectOne;
        }

    }
}
