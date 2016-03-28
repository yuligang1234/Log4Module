using System;
using System.Data;
using System.Data.SqlClient;
using Napoleon.Db;
using Napoleon.Log4Module.Log.Model;

namespace Napoleon.Log4Module.Log.DAL
{
    public static class UserDao
    {

        /// <summary>
        ///  写入数据库
        /// </summary>
        /// <param name="log">日志类</param>
        /// Author  : Napoleon
        /// Created : 2015-01-07 14:21:15
        public static void InsertLogIntoDb(SystemLog log)
        {
            string sql = "Insert into System_Log(UserName,IpAddress,OperateTime,OperateType,OperateUrl,OperateContent) values(@UserName,@IpAddress,@OperateTime,@OperateType,@OperateUrl,@OperateContent)";
            DbHelper.ExecuteSql(sql, log);
        }

        /// <summary>
        ///  获取日志列表
        /// </summary>
        /// <param name="log">log</param>
        /// <param name="startCount">startCount</param>
        /// <param name="endCount">endCount</param>
        /// <param name="startTime">starTime</param>
        /// <param name="endTime">endTime</param>
        /// Author  : Napoleon
        /// Created : 2015-01-13 09:37:54
        public static DataTable SelectLog(this SystemLog log, string startTime, string endTime, int startCount, int endCount)
        {
            string sql = "SELECT Id,UserName,IpAddress,OperateTime,OperateType,OperateUrl,OperateContent FROM (SELECT ROW_NUMBER() OVER (ORDER BY OperateTime DESC) AS number,* FROM dbo.System_Log where UserName like @UserName and IpAddress like @IpAddress and OperateType like @OperateType and OperateUrl like @OperateUrl and OperateContent like @OperateContent and OperateTime>@StartTime and OperateTime<@EndTime) AS news WHERE news.number >@StartCount AND news.number <=@EndCount";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName",string.Format("%{0}%",log.UserName)),
                new SqlParameter("@IpAddress",string.Format("%{0}%",log.IpAddress)), 
                new SqlParameter("@OperateType",string.Format("%{0}%",log.OperateType)),
                new SqlParameter("@OperateUrl",string.Format("%{0}%",log.OperateUrl)), 
                new SqlParameter("@OperateContent",string.Format("%{0}%",log.OperateContent)),
                new SqlParameter("@StartTime",startTime),
                new SqlParameter("@EndTime",endTime),
                new SqlParameter("@StartCount",startCount),
                new SqlParameter("@EndCount",endCount) 
            };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            return dt;
        }

        /// <summary>
        ///  总数
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-17 10:02:42
        public static int LogCount(this SystemLog log, string startTime, string endTime)
        {
            string sql = "SELECT count(*) FROM dbo.System_Log where UserName like @UserName and IpAddress like @IpAddress and OperateType like @OperateType and OperateUrl like @OperateUrl and OperateContent like @OperateContent and OperateTime>@StartTime and OperateTime<@EndTime";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName",string.Format("%{0}%",log.UserName)),
                new SqlParameter("@IpAddress",string.Format("%{0}%",log.IpAddress)), 
                new SqlParameter("@OperateType",string.Format("%{0}%",log.OperateType)),
                new SqlParameter("@OperateUrl",string.Format("%{0}%",log.OperateUrl)), 
                new SqlParameter("@OperateContent",string.Format("%{0}%",log.OperateContent)),
                new SqlParameter("@StartTime",startTime),
                new SqlParameter("@EndTime",endTime) 
            };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        /// <summary>
        ///  获取日志列表
        /// </summary>
        /// <param name="log">log</param>
        /// <param name="startTime">starTime</param>
        /// <param name="endTime">endTime</param>
        /// Author  : Napoleon
        /// Created : 2015-01-13 09:37:54
        public static DataTable SelectLogTable(this SystemLog log, string startTime, string endTime)
        {
            string sql = "SELECT Id,UserName,IpAddress,OperateTime,OperateType,OperateUrl,OperateContent FROM dbo.System_Log where UserName like @UserName and IpAddress like @IpAddress and OperateType like @OperateType and OperateUrl like @OperateUrl and OperateContent like @OperateContent and OperateTime>@StartTime and OperateTime<@EndTime ";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName",string.Format("%{0}%",log.UserName)),
                new SqlParameter("@IpAddress",string.Format("%{0}%",log.IpAddress)), 
                new SqlParameter("@OperateType",string.Format("%{0}%",log.OperateType)),
                new SqlParameter("@OperateUrl",string.Format("%{0}%",log.OperateUrl)), 
                new SqlParameter("@OperateContent",string.Format("%{0}%",log.OperateContent)),
                new SqlParameter("@StartTime",startTime),
                new SqlParameter("@EndTime",endTime)
            };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            return dt;
        }

        /// <summary>
        ///  根据id查询日志
        /// </summary>
        /// <param name="id">ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-16 09:53:01
        public static SystemLog GetSystemLog(string id)
        {
            string sql = "select Id,UserName,IpAddress,OperateTime,OperateType,OperateUrl,OperateContent FROM dbo.System_Log where Id=@Id";
            SystemLog log = DbHelper.GetEnumerable<SystemLog>(sql, new { @Id = id });
            return log;
        }

    }
}
