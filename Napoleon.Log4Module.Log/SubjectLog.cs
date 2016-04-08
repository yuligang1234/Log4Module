using Napoleon.Log4Module.Log.Model;

namespace Napoleon.Log4Module.Log
{

    /// <summary>
    ///  委托充当操作接口类
    /// </summary>
    /// Author  : Napoleon
    /// Created : 2016-03-26 22:19:45
    public delegate void NotifyEventHandler(object sender);

    //抽象日志类
    public class SubjectLogs
    {

        public SystemLog Log { get; set; }

        private NotifyEventHandler _notifyEvent;

        protected SubjectLogs(SystemLog log)
        {
            Log = log;
        }

        public void AddObserver(NotifyEventHandler neh)
        {
            _notifyEvent += neh;
        }

        public void RemoveObserver(NotifyEventHandler neh)
        {
            _notifyEvent -= neh;
        }

        /// <summary>
        ///  主题通知观察者
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2016-04-08 21:15:01
        public void Update()
        {
            if (_notifyEvent != null)
            {
                _notifyEvent(this);
            }
        }

    }

    //具体日志类
    public class Logs : SubjectLogs
    {
        public Logs(SystemLog log)
            : base(log)
        {

        }
    }
}
