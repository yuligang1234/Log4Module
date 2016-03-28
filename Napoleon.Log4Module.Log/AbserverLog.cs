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
    public class AbstractLogs
    {

        public SystemLog Log { get; set; }

        private NotifyEventHandler _notifyEvent;

        protected AbstractLogs(SystemLog log)
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

        public void Update()
        {
            if (_notifyEvent != null)
            {
                _notifyEvent(this);
            }
        }

    }

    //具体日志类
    public class Logs : AbstractLogs
    {
        public Logs(SystemLog log)
            : base(log)
        {
            
        }
    }
}
