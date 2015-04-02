
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

    }
}
