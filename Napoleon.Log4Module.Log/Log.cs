using Napoleon.Log4Module.Log.Common;
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
        /// <param name="url">bearychat的url</param>
        /// <param name="text">bearychat的内容(text和url都不为空,则推送)</param>
        /// Author  : Napoleon
        /// Created : 2015-01-07 15:02:15
        public static void InsertLog(this SystemLog log, LogType logType, InsertType insertType, string url, string text)
        {
            AbstractLogs logs = new Logs(log);
            OperateLog operateLog = new OperateLog(logType, url, text);
            switch (insertType)
            {
                case InsertType.DataBase:
                    logs.AddObserver(operateLog.InsertDataBase);
                    break;
                case InsertType.Txt:
                    logs.AddObserver(operateLog.InsertTxt);
                    break;
                case InsertType.All:
                    logs.AddObserver(operateLog.InsertTxt);
                    logs.AddObserver(operateLog.InsertDataBase);
                    break;
                default:
                    logs.AddObserver(operateLog.InsertTxt);
                    break;
            }
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(text))
            {
                logs.AddObserver(operateLog.PushMessage);
            }
            logs.Update();
        }

    }
}
