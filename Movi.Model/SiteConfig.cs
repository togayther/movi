using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movi.Model
{
    /// <summary>
    /// 站点配置
    /// </summary>
    public class SiteConfig
    {

        #region 站点基本设置

        private string _sitename = "派普影视";

        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName
        {
            get { return _sitename; }
            set { _sitename = value; }
        }

        private string _sitelogo = "/Content/Images/Logo.png";

        /// <summary>
        /// 站点Logo
        /// </summary>
        public string SiteLogo
        {
            get { return _sitelogo; }
            set { _sitelogo = value; }
        }

        private string _siteurl = "";

        /// <summary>
        /// 站点域名
        /// </summary>
        public string SiteUrl
        {
            get { return _siteurl; }
            set { _siteurl = value; }
        }

        private string _sitephone = "13540312451";

        /// <summary>
        /// 联系电话
        /// </summary>
        public string SitePhone
        {
            get { return _sitephone; }
            set { _sitephone = value; }
        }

        private string _siteemail = "departyment@outlook.com";

        /// <summary>
        /// 站点邮箱
        /// </summary>
        public string SiteEmail
        {
            get { return _siteemail; }
            set { _siteemail = value; }
        }

        private string _sitecrod = "";

        /// <summary>
        /// 站点备案号
        /// </summary>
        public string SiteCrod
        {
            get { return _sitecrod; }
            set { _sitecrod = value; }
        }

        private string _sitetitle = "";

        /// <summary>
        /// 站点标题
        /// </summary>
        public string SiteTitle
        {
            get { return _sitetitle; }
            set { _sitetitle = value; }
        }

        private string _sitekeywords = "";

        /// <summary>
        /// 站点关键字
        /// </summary>
        public string SiteKeywords
        {
            get { return _sitekeywords; }
            set { _sitekeywords = value; }
        }

        private string _sitedescr = "";

        /// <summary>
        /// 站点描述
        /// </summary>
        public string SiteDescr
        {
            get { return _sitedescr; }
            set { _sitedescr = value; }
        }

        private string _sitecopyright = "© departyment@outlook.com";

        /// <summary>
        /// 站点版权信息
        /// </summary>
        public string SiteCopyRight
        {
            get { return _sitecopyright; }
            set { _sitecopyright = value; }
        }

        /// <summary>
        /// 是否关闭站点
        /// </summary>
        public bool SiteStatus { get; set; }

        private string _sitecountcode = "";

        /// <summary>
        /// 站点统计代码
        /// </summary>
        public string SiteCountCode
        {
            get { return _sitecountcode; }
            set { _sitecountcode = value; }
        } 
        #endregion

        #region 邮件发送设置

        private string _emailsmtp = "";

        /// <summary>
        /// SMTP服务器
        /// </summary>
        public string EmailSmtp
        {
            get { return _emailsmtp; }
            set { _emailsmtp = value; }
        }

        private int _emailport = 25;

        /// <summary>
        /// SMTP端口
        /// </summary>
        public int EmailPort
        {
            get { return _emailport; }
            set { _emailport = value; }
        }

        private string _emailfrom = "";

        /// <summary>
        /// 发件人信息
        /// </summary>
        public string EmailFrom
        {
            get { return _emailfrom; }
            set { _emailfrom = value; }
        }

        private string _emailusername = "";

        /// <summary>
        /// 邮件账号
        /// </summary>
        public string EmailUserName
        {
            get { return _emailusername; }
            set { _emailusername = value; }
        }

        private string _emailpassword = "";

        /// <summary>
        /// 邮件密码
        /// </summary>
        public string EmailPassword
        {
            get { return _emailpassword; }
            set { _emailpassword = value; }
        }

        private string _emailnickname = "piep";

        /// <summary>
        /// 发件人昵称
        /// </summary>
        public string EmailNickName
        {
            get { return _emailnickname; }
            set { _emailnickname = value; }
        }

        #endregion

        #region 图片上传设置

        private string _avatarfilepath = "/Upload/Avatars/";

        /// <summary>
        /// 头像图片上传目录
        /// </summary>
        public string AvatarFilePath
        {
            get { return _avatarfilepath; }
            set { _avatarfilepath = value; }
        }

        private int _avatarheight = 100;

        /// <summary>
        /// 头像高度
        /// </summary>
        public int AvatarHeight
        {
            get { return _avatarheight; }
            set { _avatarheight = value; }
        }

        private int _avatarwidth = 100;

        /// <summary>
        /// 头像宽度
        /// </summary>
        public int AvatarWidth
        {
            get { return _avatarwidth; }
            set { _avatarwidth = value; }
        }

        private string _mediafilepath = "/Upload/Medias/";

        /// <summary>
        /// 媒体图片上传目录 
        /// </summary>
        public string MediaFilePath
        {
            get { return _mediafilepath; }
            set { _mediafilepath = value; }
        }

        private string _imgextensions = "*.jpg,*.gif,*.bmp,*.png";

        /// <summary>
        /// 图片上传类型
        /// </summary>
        public string ImgExtensions
        {
            get { return _imgextensions; }
            set { _imgextensions = value; }
        }

        private int _imguploadsize = 200;

        /// <summary>
        /// 图片上传限制大小（Byte）
        /// </summary>
        public int ImgUploadSize
        {
            get { return _imguploadsize; }
            set { _imguploadsize = value; }
        }

        private int _thumbheight = 160;

        /// <summary>
        /// 生成缩略图高
        /// </summary>
        public int ThumbHeight
        {
            get { return _thumbheight; }
            set { _thumbheight = value; }
        }

        private int _thumbwidth = 240;

        /// <summary>
        /// 生成缩略图宽
        /// </summary>
        public int ThumbWidth
        {
            get { return _thumbwidth; }
            set { _thumbwidth = value; }
        }

        #endregion

        #region 通用功能设置

        private int _visitstatdays = 7;

        /// <summary>
        /// 访问统计天数
        /// </summary>
        public int VisitStatDays {
            get { return _visitstatdays; }
            set { _visitstatdays = value; }
        }

        private string _defaultavatar = "~/Upload/Avatars/avatar.jpg";

        /// <summary>
        /// 默认用户头像
        /// </summary>
        public string DefaultAvatar
        {
            get { return _defaultavatar; }
            set { _defaultavatar = value; }
        }

        private int _showpagesize = 20;

        /// <summary>
        /// 每页显示像素
        /// </summary>
        public int ShowPageSize
        {
            get { return _showpagesize; }
            set { _showpagesize = value; }
        }

        private string _encryptstring = "kangming!@#$%";

        public SiteConfig()
        {
            SiteStatus = false;
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        public string EncryptString
        {
            get { return _encryptstring; }
            set { _encryptstring = value; }
        }

        public List<string> EntityCategories { get; set; }

        public List<string> EntityAreas { get; set; }

        public List<string> EntityYears { get; set; }

        #endregion

        public override string ToString()
        {
            return this.SiteName;
        }
    }
}
