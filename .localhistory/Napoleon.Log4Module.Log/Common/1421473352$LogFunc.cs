using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Napoleon.Log4Module.Log.Common
{
    public static class LogFunc
    {

        /// <summary>
        ///  RC2解密
        /// </summary>
        /// <param name="value">密文</param>
        /// <param name="key">密钥</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-12-11 14:31:15
        public static string DecrypteRc2(this string value, string key)
        {
            RC2CryptoServiceProvider mRc2Provider = new RC2CryptoServiceProvider();
            ICryptoTransform cryptoTransform = mRc2Provider.CreateDecryptor(Encoding.Default.GetBytes(key),
                LogField.MbtIv);
            return DecrypteMethod(value, cryptoTransform);
        }

        /// <summary>
        ///  AES加密
        /// </summary>
        /// <param name="value">明文</param>
        /// <param name="key">密钥</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-12-11 13:44:46
        public static string EncrypteAes(this string value, string key)
        {
            Rijndael aes = Rijndael.Create();
            ICryptoTransform cryptoTransform = aes.CreateEncryptor(Encoding.Default.GetBytes(key),
                LogField.MbtIv);
            return EncrypteMethod(value, cryptoTransform);
        }

        /// <summary>
        ///  密钥算法通用加密方法
        /// </summary>
        /// <param name="value">明文</param>
        /// <param name="cryptoTransform">加密器对象</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-12-11 14:44:11
        private static string EncrypteMethod(string value, ICryptoTransform cryptoTransform)
        {
            byte[] mbtEncryptString = Encoding.Default.GetBytes(value);
            MemoryStream mstream = new MemoryStream();
            CryptoStream mcstream = new CryptoStream(mstream, cryptoTransform, CryptoStreamMode.Write);
            mcstream.Write(mbtEncryptString, 0, mbtEncryptString.Length);
            mcstream.FlushFinalBlock();
            string encrypt = Convert.ToBase64String(mstream.ToArray());
            mstream.Close();
            mstream.Dispose();
            mcstream.Close();
            mcstream.Dispose();
            return encrypt;
        }

        /// <summary>
        ///  密钥算法通用解密方法
        /// </summary>
        /// <param name="value">密文</param>
        /// <param name="cryptoTransform">加密器对象</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-12-11 14:44:11
        private static string DecrypteMethod(string value, ICryptoTransform cryptoTransform)
        {
            byte[] mbtEncryptString = Convert.FromBase64String(value);
            MemoryStream mstream = new MemoryStream();
            CryptoStream mcstream = new CryptoStream(mstream, cryptoTransform, CryptoStreamMode.Write);
            mcstream.Write(mbtEncryptString, 0, mbtEncryptString.Length);
            mcstream.FlushFinalBlock();
            string decrypt = Encoding.Default.GetString(mstream.ToArray());
            mstream.Close();
            mstream.Dispose();
            mcstream.Close();
            mcstream.Dispose();
            return decrypt;
        }

    }
}
