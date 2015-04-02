
using System;
using System.Collections.Generic;
using System.Data;
using Napoleon.Log4Module.Log.Common;
using Napoleon.Log4Module.Log.DAL;
using Napoleon.Log4Module.Log.Model;

namespace Napoleon.Log4Module.Log
{

    public static class Log
    {

        /// <summary>
        ///  记录日志
        /// </summary>
        /// <param name="log">日志类</param>
        /// <param name="logType">error/info</param>
        /// <param name="insertType">0-表示只写入数据库，1-表示只写入文本，2-表示写入数据库/文本</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-07 15:02:15
        public static void InsertLog(this SystemLog log, LogType logType, InsertType insertType)
        {
            switch (insertType)
            {
                case InsertType.DataBase:
                    UserDao.InsertLogIntoDb(log);
                    break;
                case InsertType.Txt:
                    UserDao.InsertLogIntoTxt(log, logType);
                    break;
                case InsertType.All:
                    UserDao.InsertLogIntoDb(log);
                    UserDao.InsertLogIntoTxt(log, logType);
                    break;
            }
        }

        /// <summary>
        ///  获取datagrid使用的列表数据
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="page">The page.</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2015-01-17 10:43:36
        public static Dictionary<int, DataTable> GetLogDataGrid(this SystemLog log, string startTime, string endTime, int rows, int page)
        {
            DataTable dt = UserDao.SelectLog(log, startTime, endTime, rows * (page - 1), rows * page);
            int total = UserDao.LogCount(log, startTime, endTime);
            Dictionary<int, DataTable> dictionary = new Dictionary<int, DataTable>();
            dictionary.Add(total, dt);
            return dictionary;
        }

    }
}
