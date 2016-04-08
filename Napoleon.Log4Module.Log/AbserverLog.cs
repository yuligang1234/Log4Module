using System;
using System.Collections.Generic;
using System.IO;
using Napoleon.Log4Module.Log.Common;
using Napoleon.Log4Module.Log.DAL;
using Napoleon.Log4Module.Log.Model;
using Napoleon.PublicCommon.File;
using Napoleon.PublicCommon.Http;
using Newtonsoft.Json;

namespace Napoleon.Log4Module.Log
{
    public class AbserverLog
    {

        private LogType logType { get; set; }

        /// <summary>
        ///  https://hook.bearychat.com/=bw7GT/incoming/ab8cec4f43c2707e535b75450063ab49
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2016-03-26 22:44:06
        private string url { get; set; }

        /// <summary>
        ///  台州市交通信用评价系统
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2016-03-26 22:44:29
        private string text { get; set; }

        public AbserverLog(LogType logType, string url, string text)
        {
            this.logType = logType;
            this.url = url;
            this.text = text;
        }

        /// <summary>
        ///  写入文件夹
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2016-03-26 22:09:40
        public void InsertTxt(object obj)
        {
            SystemLog log = ((SubjectLogs)obj).Log;
            string strPath;
            switch (logType)
            {
                case LogType.Error:
                    strPath = "Log/Error/";
                    break;
                case LogType.Info:
                    strPath = "Log/Info/";
                    break;
                default:
                    strPath = "Log/Error/";
                    break;
            }
            strPath += DateTime.Now.ToString("yyyyMM") + "/";
            strPath = strPath.OpenFloder();
            strPath += DateTime.Now.ToString("yyyyMMdd") + ".txt";
            strPath = strPath.OpenFile();
            using (StreamWriter writer = new StreamWriter(strPath, true))
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
        ///  写入数据库
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2016-03-26 22:11:04
        public void InsertDataBase(object obj)
        {
            SystemLog log = ((SubjectLogs)obj).Log;
            UserDao.InsertLogIntoDb(log);
        }

        /// <summary>
        ///  推送消息到BearyChat
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2016-03-26 22:12:00
        public void PushMessage(object obj)
        {
            SystemLog log = ((SubjectLogs)obj).Log;
            List<Messages> messages = new List<Messages>();
            messages.Add(new Messages { color = "#ff0033", text = "用户IP:" + log.IpAddress });
            messages.Add(new Messages { color = "#ff0033", text = "账号信息:" + log.UserName });
            messages.Add(new Messages { color = "#ff0033", text = "日志信息:" + log.OperateContent });
            messages.Add(new Messages { color = "#ff0033", text = "日志类型:" + log.OperateType });
            messages.Add(new Messages { color = "#ff0033", text = "日志时间:" + log.OperateTime });
            messages.Add(new Messages { color = "#ff0033", text = "日志地址:" + log.OperateUrl });
            var json = "{\"text\":\"" + text + "\",\"attachments\":" + JsonConvert.SerializeObject(messages) + "}";
            url.PostJsonData(json, contentType: ContentTypes.JsonType);
        }

    }
}
