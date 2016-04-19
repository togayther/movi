using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace Movi.Common
{
    public class CryptoHelper
    {
        #region MD5加密

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns></returns>
        public static string MD5Encrypt(string plainText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.Unicode.GetBytes(plainText);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }
            return byte2String;

            //以下方法亦可
            //string MD5encryptStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(
            //    plainText, "MD5");
            //return MD5encryptStr;
        }

        #endregion

        #region DES加密

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string DESEncrypt(string plainText)
        {
            return DESEncrypt(plainText, "Mcmurphy");
        }

        /// <summary> 
        /// DES加密
        /// </summary> 
        /// <param name="plainText"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DESEncrypt(string plainText, string sKey)
        {
            var des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(plainText);
            var hashPasswordForStoringInConfigFile =
                System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5");
            if (hashPasswordForStoringInConfigFile != null)
                des.Key = Encoding.ASCII.GetBytes(hashPasswordForStoringInConfigFile.Substring(0, 8));

            var passwordForStoringInConfigFile =
                System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5");
            if (passwordForStoringInConfigFile != null)
                des.IV = Encoding.ASCII.GetBytes(passwordForStoringInConfigFile.Substring(0, 8));

            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region DES解密


        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string DESDecrypt(string plainText)
        {
            return DESDecrypt(plainText, "Mcmurphy");
        }

        /// <summary> 
        /// DES解密
        /// </summary> 
        /// <param name="plainText"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DESDecrypt(string plainText, string sKey)
        {
            var des = new DESCryptoServiceProvider();
            int len = plainText.Length/2;
            var inputByteArray = new byte[len];
            int x;
            for (x = 0; x < len; x++)
            {
                int i = Convert.ToInt32(plainText.Substring(x*2, 2), 16);
                inputByteArray[x] = (byte) i;
            }
            var hashPasswordForStoringInConfigFile =
                System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5");
            if (hashPasswordForStoringInConfigFile != null)
                des.Key = Encoding.ASCII.GetBytes(hashPasswordForStoringInConfigFile.Substring(0, 8));

            var passwordForStoringInConfigFile =
                System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5");
            if (passwordForStoringInConfigFile != null)
                des.IV = Encoding.ASCII.GetBytes(passwordForStoringInConfigFile.Substring(0, 8));

            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

        #region TripleDES加密

        /// <summary>
        /// TripleDES加密
        /// </summary>
        public static string TripleDESEncrypting(string strSource)
        {
            try
            {
                byte[] bytIn = Encoding.Default.GetBytes(strSource);
                byte[] key = {
                                 42, 16, 93, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 20, 155, 112, 2, 94, 11, 204, 119
                                 ,
                                 35, 184, 197
                             }; //定义密钥
                byte[] IV = {55, 103, 246, 79, 36, 99, 167, 3}; //定义偏移量
                var TripleDES = new TripleDESCryptoServiceProvider {IV = IV, Key = key};
                ICryptoTransform encrypto = TripleDES.CreateEncryptor();
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();
                byte[] bytOut = ms.ToArray();
                return Convert.ToBase64String(bytOut);
            }
            catch (Exception ex)
            {
                throw new Exception("加密时候出现错误!错误提示:\n" + ex.Message);
            }
        }

        #endregion

        #region TripleDES解密

        /// <summary>
        /// TripleDES解密
        /// </summary>
        public static string TripleDESDecrypting(string Source)
        {
            try
            {
                byte[] bytIn = Convert.FromBase64String(Source);
                byte[] key = {
                                 42, 16, 93, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 20, 155, 112, 2, 94, 11, 204, 119
                                 ,
                                 35, 184, 197
                             }; //定义密钥
                byte[] IV = {55, 103, 246, 79, 36, 99, 167, 3}; //定义偏移量
                var TripleDES = new TripleDESCryptoServiceProvider {IV = IV, Key = key};
                ICryptoTransform encrypto = TripleDES.CreateDecryptor();
                var ms = new MemoryStream(bytIn, 0, bytIn.Length);
                var cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                var strd = new StreamReader(cs, Encoding.Default);
                return strd.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception("解密时候出现错误!错误提示:\n" + ex.Message);
            }
        }

        #endregion

    }
}