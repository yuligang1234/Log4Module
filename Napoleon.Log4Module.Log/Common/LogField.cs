
namespace Napoleon.Log4Module.Log.Common
{
    /// <summary>
    ///  写入方式
    /// </summary>
    /// Author  : Napoleon
    /// Created : 2015-01-07 15:51:21
    public enum InsertType
    {
        DataBase,//只写入数据库
        Txt,//只写入文本
        All//写入数据库/文本
    }

    /// <summary>
    ///  日志类型（error/info）
    /// </summary>
    /// Author  : Napoleon
    /// Created : 2015-01-07 15:51:21
    public enum LogType
    {
        Error,//错误
        Info//信息
    }

    public static class LogField
    {

        #region 加解密

        /// <summary>
        ///  RC2加/解密次数
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-12-11 13:41:01
        public static int Rc2Number = 1;

        /// <summary>
        ///  对称加密算法的初始化向量(IV)
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-12-11 14:46:59
        public static byte[] MbtIv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        #endregion

    }
}
