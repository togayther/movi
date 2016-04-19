using System.Text;

namespace Movi.Common
{
    public class MailHelper
    {
        public MailHelper()
        {

        }

        private string _username;
        private string _recipient;

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 邮件正文
        /// </summary>
        public string Body { get; set; }


        /// <summary>
        /// 发件人地址
        /// </summary>
        public string From { get; set; }


        /// <summary>
        /// 发件人姓名
        /// </summary>
        public string FromName { get; set; }


        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// 邮箱域
        /// </summary>
        public string MailDomain { get; set; }

        /// <summary>
        /// 邮件服务器端口号
        /// </summary>	
        public int MailDomainPort { get; set; }


        /// <summary>
        /// SMTP认证时使用的用户名
        /// </summary>
        public string MailServerUserName
        {
            set {
                _username = value.Trim() != "" ? value.Trim() : "";
            }
            get
            {
                return _username;
            }
        }

        /// <summary>
        /// SMTP认证时使用的密码
        /// </summary>
        public string MailServerPassWord { get; set; }

        /// <summary>
        ///  是否Html邮件
        /// </summary>
        public bool Html { get; set; }


        //收件人的邮箱地址
        public bool AddRecipient(params string[] username)
        {
            _recipient = username[0].Trim();
            return true;
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        public bool Send()
        {
            var myEmail = new System.Net.Mail.MailMessage();
            Encoding eEncod = Encoding.GetEncoding("utf-8");
            myEmail.From = new System.Net.Mail.MailAddress(this.From, this.Subject, eEncod);
            myEmail.To.Add(_recipient);
            myEmail.Subject = Subject;
            myEmail.IsBodyHtml = Html;
            myEmail.Body = Body;
            myEmail.Priority = System.Net.Mail.MailPriority.Normal;
            myEmail.BodyEncoding = Encoding.GetEncoding("utf-8");
            var smtp = new System.Net.Mail.SmtpClient
                           {
                               Host = MailDomain,
                               Port = MailDomainPort,
                               Credentials = new System.Net.NetworkCredential(MailServerUserName, MailServerPassWord)
                           };
            //当不是25端口(gmail:587)
            if (MailDomainPort != 25)
            {
                smtp.EnableSsl = true;
            }
            try
            {
                smtp.Send(myEmail);
            }
            catch (System.Net.Mail.SmtpException e)
            {
                return false;
            }

            return true;
        }
    }
}
