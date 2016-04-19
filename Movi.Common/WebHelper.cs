using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Movi.Common
{
    public static class WebHelper
    {
        private const string DefaultUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.117 Safari/537.36";
        private const int DefaultTimeOut = 5000;
        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>
        /// <returns></returns>  
        public static string CreateGetHttpRequest(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Timeout = DefaultTimeOut;
            req.UserAgent = DefaultUserAgent;
            req.Referer = "http://www.baidu.com";
            req.ContentType = "text/html; charset=utf-8";
            using (var response = req.GetResponse())
            {
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    var reader = new StreamReader(stream);
                    var result = reader.ReadToEnd();
                    return result;
                }
            }
            return String.Empty;
        }

        /// <summary>
        /// 创建POST方式的HTTP请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">请求参数[key1=value1]</param>
        /// <returns></returns>
        public static string CreatePostHttpRequest(string url, string param)
        {
            var datas = Encoding.ASCII.GetBytes(param);
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.UserAgent = DefaultUserAgent;
            req.Timeout = DefaultTimeOut;
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = datas.Length;
            req.ContentType = "text/html; charset=utf-8";
            req.Referer = "http://www.baidu.com";
            using (var reqStream = req.GetRequestStream())
            {
                reqStream.Write(datas, 0, datas.Length);
            }

            using (var response = req.GetResponse())
            {
                var stream = response.GetResponseStream();
                if (stream!=null)
                {
                    var reader = new StreamReader(stream);
                    var result = reader.ReadToEnd();
                    return result;
                }
            }
            return String.Empty;
        }

        /// <summary>
        /// 下载远程图片保存至本地
        /// </summary>
        /// <param name="remoteUrl"></param>
        /// <param name="savePath"></param>
        public static void DownRemoteImageToLocal(string remoteUrl, string savePath)
        {
            var request = (HttpWebRequest)WebRequest.Create(remoteUrl);
            var response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();
            if (stream!=null)
            {
                var img = Image.FromStream(stream);
                var newimg = new Bitmap(img);
                newimg.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
