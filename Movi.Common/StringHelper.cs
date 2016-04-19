using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Movi.Common
{
    public class StringHelper
    {
        /// <summary>
        /// 获取Guid字符串值
        /// </summary>
        /// <returns></returns>
        public static string GetGuidString()
        {
            /*
             *  Guid.NewGuid().ToString("N") 结果为：
                   38bddf48f43c48588e0d78761eaa1ce6
                Guid.NewGuid().ToString("D") 结果为：
                   57d99d89-caab-482a-a0e9-a0a803eed3ba
                Guid.NewGuid().ToString("B") 结果为：
                   {09f140d5-af72-44ba-a763-c861304b46f8}
                Guid.NewGuid().ToString("P") 结果为：
                   (778406c2-efff-4262-ab03-70a77d09c2b5)
             */
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 转换字符串为List
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="speater">分隔符</param>
        /// <param name="toLower">是否转换为小写</param>
        /// <returns></returns>
        public static List<string> ConvertStrToList(string str, char speater, bool toLower = false)
        {
            var list = new List<string>();
            var ss = str.Split(speater);
            foreach (var s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != speater.ToString(CultureInfo.InvariantCulture))
                {
                    var strVal = s;
                    if (toLower)
                    {
                        strVal = s.ToLower();
                    }
                    list.Add(strVal);
                }
            }
            return list;
        }

        /// <summary>
        /// 转换字符串为字符串数组。以英文逗号为标志
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] ConvertStrToArray(string str)
        {
            return str.Split(new[] { ',' });
        }

        /// <summary>
        /// 转换List为字符串
        /// </summary>
        /// <param name="list"></param>
        /// <param name="speater"></param>
        /// <returns></returns>
        public static string ConvertListToStr(List<string> list, string speater)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 转换数组为字符串。以英文逗号分隔
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetIntListToStr(List<int> list)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 转换字典的值为字符串，以英文逗号分隔
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetDictionaryValueStr(Dictionary<int, int> list)
        {
            var sb = new StringBuilder();
            foreach (var kvp in list)
            {
                sb.Append(kvp.Value + ",");
            }
            return list.Count > 0 ? DelLastComma(sb.ToString()) : "";
        }

        /// <summary>
        /// 删除字符串结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(",", StringComparison.Ordinal));
        }

        /// <summary>
        /// 删除字符串结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar, StringComparison.Ordinal));
        }

        /// <summary>
        /// 把字符串按照指定分隔符装成 List 去除重复
        /// </summary>
        /// <param name="oStr"></param>
        /// <param name="sepeater"></param>
        /// <returns></returns>
        public static List<string> GetSubStringList(string oStr, char sepeater)
        {
            var ss = oStr.Split(sepeater);
            return ss.Where(s => !string.IsNullOrEmpty(s) && s != sepeater.ToString(CultureInfo.InvariantCulture)).ToList();
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="splitstr">分隔符</param>
        /// <returns>返回分割后的字符串数组</returns>
        public static string[] SplitMulti(string str, string splitstr)
        {
            string[] strArray = null;
            if (!string.IsNullOrEmpty(str))
            {
                strArray = new Regex(splitstr).Split(str);
            }
            return strArray;
        }

        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="inputString">参数字符串</param>
        /// <returns></returns>
        public static int StrLength(string inputString)
        {
            var ascii = new ASCIIEncoding();
            var tempLen = 0;
            var s = ascii.GetBytes(inputString);
            foreach (var t in s)
            {
                if (t == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }
        
        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="inputString">要处理的字符串</param>
        /// <param name="len">指定长度</param>
        /// <returns>返回处理后的字符串</returns>
        public static string CutString(string inputString, int len)
        {
            var isShowFix = false;
            if (len % 2 == 1)
            {
                isShowFix = true;
                len--;
            }
            var ascii = new ASCIIEncoding();
            var tempLen = 0;
            var tempString = "";
            var s = ascii.GetBytes(inputString);
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }

            var mybyte = Encoding.Default.GetBytes(inputString);
            if (isShowFix && mybyte.Length > len)
                tempString += "…";
            return tempString;
        }

        /// <summary>
        /// 生成指定长度随机字符串(默认为6)
        /// </summary>
        /// <param name="codeCount"></param>
        /// <returns></returns>
        public static string GetRandomString(int codeCount = 6)
        {
            var str = string.Empty;
            var rep = 0;
            var num2 = DateTime.Now.Ticks + rep;
            rep++;
            var random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (var i = 0; i < codeCount; i++)
            {
                char ch;
                var num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch;
            }
            return str;
        }
    }
}
