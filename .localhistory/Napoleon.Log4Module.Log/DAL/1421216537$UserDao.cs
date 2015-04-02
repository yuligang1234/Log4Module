using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using Napoleon.Log4Module.Log.Common;
using Napoleon.Log4Module.Log.Helper;
using Napoleon.Log4Module.Log.Model;

namespace Napoleon.Log4Module.Log.DAL
{
    public static class UserDao
    {

        /// <summary>
        ///  写入数据库
        /// </summary>
        /// <param name="log">日志类</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-07 14:21:15
        public static void InsertLogIntoDb(SystemLog log)
        {
            string sql = string.Format("Insert into System_Log(UserName,IpAddress,OperateTime,OperateType,OperateUrl,OperateContent) values('{0}','{1}','{2}','{3}','{4}','{5}')", log.UserName, log.IpAddress, log.OperateTime, log.OperateType, log.OperateUrl, log.OperateContent);
            SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        ///  写入文本
        /// </summary>
        /// <param name="log">日志类</param>
        /// <param name="logType">error/info</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-07 14:21:34
        public static void InsertLogIntoTxt(SystemLog log, LogType logType)
        {
            string folder = "";
            switch (logType)
            {
                case LogType.Error:
                    folder = "~/Log/Error/";
                    break;
                case LogType.Info:
                    folder = "~/Log/Info/";
                    break;
            }
            //创建文件夹
            folder = folder + DateTime.Now.ToString("yyyyMM") + "/";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(folder)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folder));
            }
            //创建文件
            string path = folder + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            if (!File.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter writer = File.AppendText(HttpContext.Current.Server.MapPath(path)))
            {
                writer.WriteLine("操作用户: {0}", log.UserName);
                writer.WriteLine("用户IP: {0}", log.IpAddress);
                writer.WriteLine("操作时间: {0}", log.OperateTime);
                writer.WriteLine("操作类型: {0}", log.OperateType);
                writer.WriteLine("操作地址: {0}", log.OperateUrl);
                writer.WriteLine("操作内容: {0}", log.OperateContent);
                writer.WriteLine("____________________________________________________________________");
                writer.Flush();
                writer.Close();
            }
        }

        /// <summary>
        ///  获取日志列表
        /// </summary>
        /// <param name="log">log</param>
        /// <param name="rows">rows</param>
        /// <param name="page">page</param>
        /// <param name="startTime">starTime</param>
        /// <param name="endTime">endTime</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-13 09:37:54
        public static List<SystemLog> SelectLog(this SystemLog log, string startTime, string endTime, int rows, int page)
        {
            string sql = string.Format("SELECT Id,UserName,IpAddress,OperateTime,OperateType,OperateUrl,OperateContent FROM (SELECT ROW_NUMBER() OVER (ORDER BY OperateTime DESC) AS number,* FROM dbo.System_Log where UserName like '%{0}' and IpAddress like '%{1}' and OperateType like '%{2}' and OperateUrl like '%{3}' and OperateContent like '%{4}' and OperateTime>'{5}' and OperateTime<'{6}') AS news WHERE news.number >{7} AND news.number <{8}", log.UserName, log.IpAddress, log.OperateType, log.OperateUrl, log.OperateContent, startTime, endTime, rows * page, rows * (page + 1));
            List<SystemLog> logs = SqlHelper.GetEnumerables<SystemLog>(sql);
            return logs;
        }

        /// <summary>
        ///  获取日志列表
        /// </summary>
        /// <param name="log">log</param>
        /// <param name="rows">rows</param>
        /// <param name="page">page</param>
        /// <param name="startTime">starTime</param>
        /// <param name="endTime">endTime</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-13 09:37:54
        public static DataTable SelectLogTable(this SystemLog log, string startTime, string endTime, int rows, int page)
        {
            string sql = string.Format("SELECT Id,UserName,IpAddress,OperateTime,OperateType,OperateUrl,OperateContent FROM (SELECT ROW_NUMBER() OVER (ORDER BY OperateTime DESC) AS number,* FROM dbo.System_Log where UserName like '%{0}' and IpAddress like '%{1}' and OperateType like '%{2}' and OperateUrl like '%{3}' and OperateContent like '%{4}' and OperateTime>'{5}' and OperateTime<'{6}') AS news WHERE news.number >{7} AND news.number <{8}", log.UserName, log.IpAddress, log.OperateType, log.OperateUrl, log.OperateContent, startTime, endTime, rows * page, rows * (page + 1));
            DataTable dt = SqlHelper.GetDataTable(sql);
            return dt;
        }

    }
}
