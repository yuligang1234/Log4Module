using System;

namespace Napoleon.Log4Module.Log.Model
{
    public class SystemLog
    {

        private int _id;
        /// <summary>
        ///  Id
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 02:08:25
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _userName;
        /// <summary>
        ///  操作用户
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 02:08:25
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _ipAddress;
        /// <summary>
        ///  用户IP
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 02:08:25
        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        private DateTime _operateTime;
        /// <summary>
        ///  操作时间
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 02:08:25
        public DateTime OperateTime
        {
            get { return _operateTime; }
            set { _operateTime = value; }
        }

        private string _operateType;
        /// <summary>
        ///  操作类型
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 02:08:25
        public string OperateType
        {
            get { return _operateType; }
            set { _operateType = value; }
        }

        private string _operateUrl;
        /// <summary>
        ///  操作地址
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 02:08:25
        public string OperateUrl
        {
            get { return _operateUrl; }
            set { _operateUrl = value; }
        }

        private string _operateContent;
        /// <summary>
        ///  操作内容
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 02:08:25
        public string OperateContent
        {
            get { return _operateContent; }
            set { _operateContent = value; }
        }





    }
}
