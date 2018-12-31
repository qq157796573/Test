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
        public List<T> QueryAll<T>() where T : new()
        {
            using (SqlConnection conn = new SqlConnection(StrCon))
            {
                conn.Open();
                Type type = typeof(T);
                string sql = SqlCache<T>.GetSqlSelectAll();
                List<T> list = new List<T>();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader sdr = comm.ExecuteReader();
                if (sdr != null)
                {
                    while (sdr.Read())
                    {
                        T t = new T();
                        foreach (PropertyInfo prop in type.GetProperties())
                        {
                            prop.SetValue(t, sdr[prop.GetPropName()] is DBNull ? null : sdr[prop.GetPropName()]);
                        }
                        list.Add(t);
                    }
                }
                return list;
            }
        }
        /// <summary>
        /// 按编号查询
        /// </summary>
        /// <typeparam name="T">当前查询的类型</typeparam>
        /// <param name="id">查询的编号</param>
        /// <returns>返回查询单个当前对象</returns>
        public T QueryOne<T>(int id) where T : new()
        {
            using (SqlConnection conn = new SqlConnection(StrCon))
            {
                Type type = typeof(T);
                conn.Open();
                string sql = SqlCache<T>.GetSqlSelectOne();
                SqlCommand comm = new SqlCommand(sql, conn);

                comm.Parameters.Add(new SqlParameter($"@{type.BaseType.GetProperties().FirstOrDefault().Name}", id));
                SqlDataReader sdr = comm.ExecuteReader();

                if (sdr != null)
                    if (sdr.Read())
                    {
                        T t = new T();
                        foreach (PropertyInfo prop in type.GetProperties())
                        {
                            prop.SetValue(t, sdr[prop.GetPropName()] is DBNull ? null : sdr[prop.GetPropName()]);
                        }
                        return t;
                    }
                throw new Exception("查询出错");
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T">当前类型</typeparam>
        /// <param name="t">添加对象</param>
        /// <returns>返回是否成功</returns>
        public bool Insert<T>(T t)
        {
            using (SqlConnection conn = new SqlConnection(StrCon))
            {
                conn.Open();
                string sql = SqlCache<T>.GetSqlInsert();
                SqlCommand comm = new SqlCommand(sql, conn);
                PropertyInfo[] propList = typeof(T).GetProperties();
                propList = propList.Where(p => !typeof(T).GetProperties(BindingFlags.DeclaredOnly).Contains(p)).ToArray();
                foreach (var prop in propList)
                {
                    comm.Parameters.Add(new SqlParameter(prop.Name, prop.GetValue(t)));
                }
                return comm.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete<T>(int id)
        {
            using (SqlConnection conn = new SqlConnection(StrCon))
            {
                conn.Open();
                string sql = SqlCache<T>.GetSqlDelete();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.Add(new SqlParameter($"@{typeof(T).GetProperties(BindingFlags.DeclaredOnly).FirstOrDefault().Name}", id));
                return comm.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update<T>(T t)
        {
            using (SqlConnection conn = new SqlConnection(StrCon))
            {
                conn.Open();
                string sql = SqlCache<T>.GetSqlUpdate();
                SqlCommand comm = new SqlCommand(sql, conn);
                foreach (var prop in typeof(T).GetProperties())
                {
                    comm.Parameters.Add(new SqlParameter($"@{prop.Name}", prop.GetValue(t) ?? DBNull.Value));
                }
                return comm.ExecuteNonQuery() > 0;

            }
        }
    }
}
