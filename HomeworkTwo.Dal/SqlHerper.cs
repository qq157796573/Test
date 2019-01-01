using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HomeworkTwo.IDal;
using HomeworkTwo.Comm.ExtentsMethonds;
using HomeworkTwo.Comm.Model;

namespace HomeworkTwo.Dal
{
    public class SqlHerper : ISqlHerper
    {
        private static string StrCon = ConfigurationManager.ConnectionStrings["dbName"].ConnectionString;
        
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <typeparam name="T">当前查询的类型</typeparam>
        /// <returns>返回当前对象列表</returns>
        public IEnumerable<T> QueryAll<T>() where T : BaseModel, new()
        {
            //using (SqlConnection conn = new SqlConnection(StrCon))
            //{
            //    conn.Open();
            //    Type type = typeof(T);
            //    string sql = SqlCache<T>.GetSqlSelectAll();
            //    List<T> list = new List<T>();
            //    SqlCommand comm = new SqlCommand(sql, conn);
            //    SqlDataReader sdr = comm.ExecuteReader();
            //    if (sdr != null)
            //    {
            //        while (sdr.Read())
            //        {
            //            T t = new T();
            //            foreach (PropertyInfo prop in type.GetProperties())
            //            {
            //                prop.SetValue(t, sdr[prop.GetPropName()] is DBNull ? null : sdr[prop.GetPropName()]);
            //            }
            //            list.Add(t);
            //        }
            //    }
            //    return list;
            //}
            string sql = SqlCache<T>.GetSqlSelectAll();
            return OpenDataBaseConnection(sql, s =>
             {
                 SqlDataReader sdr = s.ExecuteReader();
                 return GetDataReader<T>(sdr);
             });
        }
        #region 通用数据库连接
        /// <summary>
        /// 通用数据库连接
        /// </summary>
        /// <typeparam name="T">需要返回的对象</typeparam>
        /// <param name="sql">执行的脚本</param>
        /// <param name="func">执行命令</param>
        /// <returns></returns>
        private T OpenDataBaseConnection<T>(string sql,Func<SqlCommand,T> func)
        {
            using (SqlConnection conn = new SqlConnection(StrCon))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                return func.Invoke(comm);
            }
        }
        #endregion

        #region SqlDataReader 读取数据
        private IEnumerable<T> GetDataReader<T>(SqlDataReader sqlDataReader)  where T: BaseModel,new()
        {
            Type type = typeof(T);
            if (sqlDataReader != null)
            {
                List<T> list = new List<T>();
                while (sqlDataReader.Read())
                {
                    T t = new T();
                    foreach (PropertyInfo prop in type.GetProperties())
                    {
                        prop.SetValue(t, sqlDataReader[prop.GetPropName()] is DBNull ? null : sqlDataReader[prop.GetPropName()]);
                    }
                    list.Add(t);
                }
                return list;
            }
                
            throw new Exception("查询出错");
        }
        #endregion
        /// <summary>
        /// 按编号查询
        /// </summary>
        /// <typeparam name="T">当前查询的类型</typeparam>
        /// <param name="id">查询的编号</param>
        /// <returns>返回查询单个当前对象</returns>
        public T QueryOne<T>(int id) where T :BaseModel ,new()
        {
            //using (SqlConnection conn = new SqlConnection(StrCon))
            //{
            //    Type type = typeof(T);
            //    conn.Open();
            //    string sql = SqlCache<T>.GetSqlSelectOne();
            //    SqlCommand comm = new SqlCommand(sql, conn);

            //    comm.Parameters.Add(new SqlParameter($"@{type.BaseType.GetProperties().FirstOrDefault().Name}", id));
            //    SqlDataReader sdr = comm.ExecuteReader();

            //    if (sdr != null)
            //        if (sdr.Read())
            //        {
            //            T t = new T();
            //            foreach (PropertyInfo prop in type.GetProperties())
            //            {
            //                prop.SetValue(t, sdr[prop.GetPropName()] is DBNull ? null : sdr[prop.GetPropName()]);
            //            }
            //            return t;
            //        }
            //    throw new Exception("查询出错");
            //}

            string sql = SqlCache<T>.GetSqlSelectOne();
            return OpenDataBaseConnection(sql, s =>
             {
                 s.Parameters.Add(new SqlParameter($"@{typeof(T).BaseType.GetProperties().FirstOrDefault().Name}", id));
                 SqlDataReader sdr = s.ExecuteReader();
                 return GetDataReader<T>(sdr).FirstOrDefault();
             });

        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T">当前类型</typeparam>
        /// <param name="t">添加对象</param>
        /// <returns>返回是否成功</returns>
        public bool Insert<T>(T t)
        {
            //using (SqlConnection conn = new SqlConnection(StrCon))
            //{
            //    conn.Open();
            //    string sql = SqlCache<T>.GetSqlInsert();
            //    SqlCommand comm = new SqlCommand(sql, conn);
            //    PropertyInfo[] propList = typeof(T).GetProperties();
            //    propList = propList.Where(p => !typeof(T).GetProperties(BindingFlags.DeclaredOnly).Contains(p)).ToArray();
            //    foreach (var prop in propList)
            //    {
            //        comm.Parameters.Add(new SqlParameter(prop.Name, prop.GetValue(t)));
            //    }
            //    return comm.ExecuteNonQuery() > 0;
            //}
          string sql = SqlCache<T>.GetSqlInsert();
          return OpenDataBaseConnection(sql, (s) => 
          {
              PropertyInfo[] propList = typeof(T).GetProperties();
              var parameters = propList.Where(p => !typeof(T).GetProperties(BindingFlags.DeclaredOnly).Contains(p)).Select(p => new SqlParameter(p.Name, p.GetValue(t))).ToArray();
              s.Parameters.AddRange(parameters);
              return s.ExecuteNonQuery()>0;
          });
        }

        public bool Delete<T>(int id)
        {
            //using (SqlConnection conn = new SqlConnection(StrCon))
            //{
            //    conn.Open();
            //    string sql = SqlCache<T>.GetSqlDelete();
            //    SqlCommand comm = new SqlCommand(sql, conn);
            //    comm.Parameters.Add(new SqlParameter($"@{typeof(T).GetProperties(BindingFlags.DeclaredOnly).FirstOrDefault().Name}", id));
            //    return comm.ExecuteNonQuery() > 0;
            //}
            string sql = SqlCache<T>.GetSqlDelete();
            return OpenDataBaseConnection(sql, s =>
            {
                s.Parameters.Add(new SqlParameter($"@{typeof(T).GetProperties(BindingFlags.DeclaredOnly).FirstOrDefault().Name}", id));
                return s.ExecuteNonQuery()>0;
            });
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update<T>(T t)
        {
            //using (SqlConnection conn = new SqlConnection(StrCon))
            //{
            //    conn.Open();
            //    string sql = SqlCache<T>.GetSqlUpdate();
            //    SqlCommand comm = new SqlCommand(sql, conn);
            //    foreach (var prop in typeof(T).GetProperties())
            //    {
            //        comm.Parameters.Add(new SqlParameter($"@{prop.Name}", prop.GetValue(t) ?? DBNull.Value));
            //    }
            //    return comm.ExecuteNonQuery() > 0;

            //}
            string sql = SqlCache<T>.GetSqlUpdate();
            return OpenDataBaseConnection(sql, s =>
             {
                 var parameters = typeof(T).GetProperties().Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t) ?? DBNull.Value)).ToArray();
                 s.Parameters.AddRange(parameters);
                 return s.ExecuteNonQuery() > 0;
             });
        }
    }
}
