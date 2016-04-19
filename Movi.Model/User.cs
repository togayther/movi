using System;
using System.ComponentModel.DataAnnotations;

namespace Movi.Model
{
    /// <summary>
    /// 管理猿
    /// </summary>
    public class User : ICloneable
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [Required(ErrorMessage = "登录名不能为空")]
        [Display(Name = "登录名")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0}的长度必须大于{2}个字符并小于{1}个字符")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "登录密码不能为空")]
        [Display(Name = "登录密码")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0}的长度必须大于{2}个字符并小于{1}个字符")]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required(ErrorMessage = "昵称不能为空")]
        [Display(Name = "昵称")]
        [DataType(DataType.Text)]
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像")]
        [DataType(DataType.Text)]
        public string Avatar { get; set; }

        /// <summary>
        /// 密盐
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 伊妹儿
        /// </summary>
        [Required(ErrorMessage = "邮箱地址不能为空")]
        [Display(Name = "邮箱")]
        [DataType(DataType.Text)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "{0}的格式不正确")]
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "联系电话")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [Display(Name = "QQ号码")]
        [DataType(DataType.Text)]
        public string Qq { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Display(Name = "添加时间")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 最近登录时间
        /// </summary>
        [Display(Name = "最近登录时间")]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 最近登录IP
        /// </summary>
        [Display(Name = "最近登录IP")]
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 是否猴王
        /// </summary>
        public bool IsSuperUser { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        [Display(Name = "状态")]
        public bool IsLock { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
