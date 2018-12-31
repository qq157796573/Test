using HomeworkTwo.Comm;
using HomeworkTwo.Comm.ExtentsMethonds;
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
        private static string tableName = null;
        /// <summary>
        /// 静态构造函数初始化当前类特性名称
        /// </summary>
        static SqlCache()
        {
            Type type = typeof(T);
            tableName = type.Name;
            NameAttribute nameAttribute = type.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
            if (nameAttribute != null)
                tableName = nameAttribute.Name;
        }
        /// <summary>
        /// 获取新增sql 语句
        /// </summary>
        /// <returns></returns>
        public static string GetSqlInsert()
        {
            if (string.IsNullOrEmpty(sqlInsert))
            {
                #region 记录
                //Type type = typeof(T);
                //List<string> columns = new List<string>();
                //List<string> values = new List<string>();
                //foreach (PropertyInfo prop in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
                //{
                //    string columnName = prop.Name;
                //    if (prop.IsDefined(typeof(NameAttribute), true))
                //    {
                //        NameAttribute nameAttribute = prop.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
                //        if (nameAttribute != null)
                //            columnName = nameAttribute.Name;
                //    }
                //    columns.Add(columnName);
                //    values.Add($"@{columnName}");
                //}
                //Type attributeType = typeof(NameAttribute);
                //NameAttribute nameAttributeTable = type.GetCustomAttribute(attributeType) as NameAttribute;
                //string tableName = type.Name;
                //if (nameAttributeTable != null)
                //    tableName = nameAttributeTable.Name;
                //sqlInsert = $@"INSERT INTO [{tableName}]({string.Join(",", columns)} )VALUES({string.Join(",", values)}";
                #endregion
                Type type = typeof(T);
                PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
                sqlInsert = $@"INSERT INTO [{tableName}]({string.Join(",", propertyInfos.Select(p => $"[{ p.GetPropName()}]"))} )VALUES({string.Join(",", propertyInfos.Select(p => $"@{p.Name}"))}";
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
                #region 记录
                //Type type = typeof(T);
                //Type attributeType = typeof(NameAttribute);
                //NameAttribute nameAttributeTable = type.GetCustomAttribute(attributeType) as NameAttribute;
                //string tableName = type.Name;
                //if (nameAttributeTable != null)
                //    tableName = nameAttributeTable.Name;
                //List<string> conlumns = new List<string>();
                //foreach (PropertyInfo prop in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
                //{
                //    string columnName = prop.Name;
                //    if (prop.IsDefined(typeof(NameAttribute), true))
                //    {
                //        NameAttribute nameAttributeProp = prop.GetCustomAttribute(typeof(NameAttribute)) as NameAttribute;
                //        if (nameAttributeProp != null)
                //            columnName = nameAttributeProp.Name;
                //    }
                //    conlumns.Add($"{columnName}=@{columnName}");
                //}
                //PropertyInfo basePropertyInfo = type.BaseType.GetProperties().FirstOrDefault();
                //string baseColumnName = basePropertyInfo.Name;
                //if (basePropertyInfo.IsDefined(typeof(NameAttribute), true))
                //{
                //    NameAttribute columnNameAttribute = basePropertyInfo.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
                //    if (null != columnNameAttribute)
                //        baseColumnName = columnNameAttribute.Name;
                //}
                //sqlUpdate = $@" UPDATE [{tableName}] SET {string.Join(",", conlumns) }
                //    WHERE {baseColumnName}=@{baseColumnName}";
                #endregion
                Type type = typeof(T);
                //获取基类中主键属性
                PropertyInfo basePropertyInfo = type.BaseType.GetProperties().FirstOrDefault();
                sqlUpdate = $@" UPDATE [{tableName}] SET {string.Join(",", type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Select(p => $"{p.GetPropName()}=@{p.Name}")) }
                    WHERE {basePropertyInfo.GetPropName()}=@{basePropertyInfo.Name}";
            }
            return sqlUpdate;
        }

        #region 获取删除sql
        /// <summary>
        /// 获取删除sql
        /// </summary>
        /// <returns></returns>
        public static string GetSqlDelete()
        {
            if (string.IsNullOrEmpty(sqlDelete))
            {
                #region 记录
                //Type type = typeof(T);
                //Type attributeType = typeof(NameAttribute);
                //NameAttribute nameAttributeTable = type.GetCustomAttribute(attributeType) as NameAttribute;
                //string tableName = type.Name;
                //if (nameAttributeTable != null)
                //    tableName = nameAttributeTable.Name;
                //PropertyInfo basePropertyInfo = type.BaseType.GetProperties().FirstOrDefault();
                //string baseColumnName = basePropertyInfo.Name;
                //if (basePropertyInfo.IsDefined(typeof(NameAttribute), true))
                //{
                //    NameAttribute columnNameAttribute = basePropertyInfo.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
                //    if (null != columnNameAttribute)
                //        baseColumnName = columnNameAttribute.Name;
                //}
                //sqlDelete = $@"DELETE FROM [{type.Name}] WHERE {baseColumnName}=@{baseColumnName}";
                #endregion
                Type type = typeof(T);
                Type attributeType = typeof(NameAttribute);
                NameAttribute nameAttributeTable = type.GetCustomAttribute(attributeType) as NameAttribute;
                string tableName = type.Name;
                if (nameAttributeTable != null)
                    tableName = nameAttributeTable.Name;
                PropertyInfo basePropertyInfo = type.BaseType.GetProperties().FirstOrDefault();
                sqlDelete = $@"DELETE FROM [{tableName}] WHERE {basePropertyInfo.GetPropName()}=@{basePropertyInfo.Name}";
            }
            return sqlDelete;
        }
        #endregion

        #region 获取所有查询sql
        /// <summary>
        /// 获取所有查询sql
        /// </summary>
        /// <returns></returns>
        public static string GetSqlSelectAll()
        {
            if (string.IsNullOrEmpty(sqlSelectAll))
            {
                Type type = typeof(T);
                #region 记录
                //Type attributeType = typeof(NameAttribute);

                //List<string> columns = new List<string>();
                //foreach (var prop in type.GetProperties())
                //{
                //    string columanName = prop.Name;
                //    if (prop.IsDefined(typeof(NameAttribute), true))
                //    {
                //        NameAttribute nameAttribute = prop.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
                //        if (null != nameAttribute)
                //            columanName = nameAttribute.Name;
                //    }
                //    columns.Add(columanName);
                //}
                #endregion
                sqlSelectAll = $@"SELECT {string.Join(",", type.GetProperties().Select(p => $"[{p.GetPropName()}]"))} FROM [{tableName}]";
            }
            return sqlSelectAll;
        }
        #endregion

        #region 获取单个查询
        /// <summary>
        /// 获取单个查询  sql
        /// </summary>
        /// <returns></returns>
        public static string GetSqlSelectOne()
        {
            if (string.IsNullOrEmpty(sqlSelectOne))
            {
                #region 记录
                //Type type = typeof(T);
                //Type attributeType = typeof(NameAttribute);
                //string name = type.BaseType.GetProperties().FirstOrDefault().Name;
                //List<string> columns = new List<string>();
                //foreach (var prop in type.GetProperties())
                //{
                //    string columanName = prop.Name;
                //    if (prop.IsDefined(typeof(NameAttribute), true))
                //    {
                //        NameAttribute nameAttribute = prop.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
                //        if (null != nameAttribute)
                //            columanName = nameAttribute.Name;
                //    }
                //    columns.Add(columanName);
                //}
                //NameAttribute nameAttributeTable = type.GetCustomAttribute(attributeType) as NameAttribute;
                //string tableName = type.Name;
                //if (nameAttributeTable != null)
                //    tableName = nameAttributeTable.Name;
                //sqlSelectOne = $@"SELECT {string.Join(",", columns)} FROM [{tableName}]
                //WHERE {name}=@{name}";
                #endregion
                Type type = typeof(T);
                PropertyInfo baseProp = type.BaseType.GetProperties().FirstOrDefault();
                sqlSelectOne = $@"SELECT {string.Join(",", type.GetProperties().Select(p => $"[{p.GetPropName()}]"))} FROM [{tableName}]
                WHERE {baseProp.GetPropName()}=@{baseProp.Name}";
            }
            return sqlSelectOne;
        }
        #endregion
    }
}
