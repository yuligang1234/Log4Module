using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Napoleon.Log4Module.Log.Common;

namespace Napoleon.Log4Module.Log.Helper
{
    public static class SqlHelper
    {

        private static string _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        /// <summary>
        ///  MSSQL链接
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-09-01 20:09:21
        //private readonly static string SqlConnection = string.Format(@"Data Source=SKY-PC\SQL2005;Initial Catalog=UserModule;User Id=sa;Password=123456;");
        private static readonly string SqlConnection = _connectionString.DecrypteRc2(LogField.Rc2);

        #region MSSQL数据库方法

        /// <summary>
        ///  打开数据库
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-01 20:18:04
        private static SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(SqlConnection);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        ///  初始化DataAdapter
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns>SqlDataAdapter.</returns>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-12-15 13:27:05
        private static SqlDataAdapter OpenDataAdapter(string sql)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql, SqlConnection);
            return adapter;
        }

        #endregion

        #region 数据库通用操作

        /// <summary>
        ///  获取DataSet
        /// </summary>
        /// <param name="sql">SQL</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-01 20:18:16
        public static DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();
            IDbDataAdapter adapter = OpenDataAdapter(sql);
            adapter.Fill(ds);
            return ds;
        }

        /// <summary>
        ///  获取DateTable
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-22 09:51:02
        public static DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            DataSet ds = GetDataSet(sql);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        ///  获取实体类集合
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="sql">SQL</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-01 20:41:32
        public static List<T> GetEnumerables<T>(string sql) where T : new()
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Query<T>(sql).ToList();
            }
        }

        /// <summary>
        ///  获取单个实体类
        /// </summary>
        /// <param name="sql">SQL.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-22 16:04:50
        public static T GetEnumerable<T>(string sql) where T : new()
        {
            if (GetEnumerables<T>(sql).Count > 0)
            {
                return GetEnumerables<T>(sql)[0];
            }
            return default(T);
        }

        /// <summary>
        ///  增加、删除、修改操作
        /// </summary>
        /// <param name="sql">SQL</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-01 21:04:20
        public static int ExecuteSql(string sql)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Execute(sql);
            }
        }

        /// <summary>
        ///  查询个数
        /// </summary>
        /// <param name="sql">SQL</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-01 21:09:47
        public static int QueryCount(string sql)
        {

            using (IDbConnection conn = OpenConnection())
            {
                int row;
                try
                {
                    row = (int)conn.ExecuteScalar(sql);
                }
                catch (Exception)
                {
                    row = 0;
                }
                return row;
            }
        }

        #endregion






    }
}
