using Ruanmou.Framework;
using Ruanmou.Libraries.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.DAL
{
    /// <summary>
    /// 数据库访问帮助类
    /// </summary>
    public class SqlHelper
    {
        private static string ConnectionStringCustomers = ConfigurationManager.ConnectionStrings["Customers"].ConnectionString;


        public Company FindCompany(int id)
        {
            string sql = $@"SELECT [Id]
                          ,[Name]
                          ,[CreateTime]
                          ,[CreatorId]
                          ,[LastModifierId]
                          ,[LastModifyTime]
                      FROM[dbo].[Company]
                     WHERE ID = {id}";
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Company company = new Company()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString()
                        //...
                    };
                    return company;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 一个方法满足不同的类型  泛型
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id)
        {
            //不同的实体T是不同的SQL  反射拼装
            Type type = typeof(T);
            //string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetMappingName()}]"));
            //string sql = $"SELECT {columnString} FROM [{type.GetMappingName()}]  WHERE ID = {id}";
            string sql = SqlBuilder<T>.GetSql(SqlType.FindOne);
            IEnumerable<SqlParameter> paraList = new List<SqlParameter>()
            {
                new SqlParameter("@Id",id)
             };
            return this.ExecuteSql<T>(sql, paraList, command =>
            {
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    T t = (T)Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        prop.SetValue(t, reader[prop.GetMappingName()] is DBNull ? null : reader[prop.GetMappingName()]);
                    }
                    return t;
                }
                else
                {
                    return default(T);
                }
            });
            //using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            //{
            //    SqlCommand command = new SqlCommand(sql, conn);
            //    command.Parameters.AddRange(paraList.ToArray());
            //    conn.Open();
            //    var reader = command.ExecuteReader();
            //    if (reader.Read())
            //    {
            //        //如果你不知道是什么类型，又需要操作，肯定是反射
            //        T t = (T)Activator.CreateInstance(type);
            //        foreach (var prop in type.GetProperties())
            //        {
            //            prop.SetValue(t, reader[prop.GetMappingName()] is DBNull ? null : reader[prop.GetMappingName()]);
            //        }
            //        return t;
            //    }
            //    else
            //    {
            //        return default(T);
            //    }
            //}
        }
        /// <summary>
        /// 不同的方法，对command的执行方式不一样，后续操作不一样
        /// 就是利用委托封装，完成代码复用---还有项目规范
        /// 这个方法封装不是Eleven首创的，是高级班的一个学员在作业中发明的，我很佩服当时还发过红包
        /// </summary>
        private T ExecuteSql<T>(string sql, IEnumerable<SqlParameter> paraList, Func<SqlCommand, T> func)
        {
            SqlTransaction trans = null;
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                try
                {
                    trans = conn.BeginTransaction();
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddRange(paraList.ToArray());
                    conn.Open();
                    T t = func.Invoke(command);
                    trans.Commit();
                    return t;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (trans != null)
                        trans.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 一个方法满足不同表的数据插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Insert<T>(T t)
        {
            Type type = typeof(T);
            //string columnStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"[{p.GetMappingName()}]"));
            //string valuesStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"@{p.GetMappingName()}"));
            ////string valuesStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"'{p.GetValue(t)}'"));
            //string sql = $@"INSERT INTO [{type.GetMappingName()}]  ({columnStrings})                         VALUES( {valuesStrings})";

            string sql = SqlBuilder<T>.GetSql(SqlType.Insert);
            IEnumerable<SqlParameter> paraList = type.GetPropertiesWithoutKey().Select(p => new SqlParameter($"@{p.GetMappingName()}", p.GetValue(t) ?? DBNull.Value));

            return this.ExecuteSql<bool>(sql, paraList, command =>
              {
                  int iResult = command.ExecuteNonQuery();
                  return iResult == 1;
              });

            //using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            //{
            //    SqlCommand command = new SqlCommand(sql, conn);
            //    command.Parameters.AddRange(paraList.ToArray());
            //    conn.Open();
            //    int iResult = command.ExecuteNonQuery();
            //    return iResult == 1;
            //}
        }

        /// <summary>
        /// 一个方法满足不同表的数据更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">要更新的实体</param>
        /// <returns></returns>
        public bool Update<T>(T t)
        {
            Type type = typeof(T);
            //string columnStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"[{p.GetMappingName()}]=@{p.GetMappingName()}"));
            IEnumerable<SqlParameter> paraList = type.GetProperties().Select(p => new SqlParameter($"@{p.GetMappingName()}", p.GetValue(t) ?? DBNull.Value));

            //string sql = $"UPDATE [{type.GetMappingName()}] SET {columnStrings} WHERE Id=@Id;";
            string sql = SqlBuilder<T>.GetSql(SqlType.Update);
            return this.ExecuteSql<bool>(sql, paraList, command =>
            {
                int iResult = command.ExecuteNonQuery();
                return iResult == 1;
            });
            //using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            //{
            //    SqlCommand command = new SqlCommand(sql, conn);
            //    command.Parameters.AddRange(paraList.ToArray());
            //    conn.Open();
            //    int iResult = command.ExecuteNonQuery();
            //    return iResult == 1;
            //}
        }
    }
}
