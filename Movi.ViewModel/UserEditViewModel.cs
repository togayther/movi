using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movi.Model;

namespace Movi.ViewModel
{
    /// <summary>
    /// 管理猿
    /// </summary>
    public class UserEditViewModel
    {
        public UserEditViewModel()
        {
        }

        public UserEditViewModel(User user)
        {
            Avatar = user.Avatar;
            Id = user.Id;
            Qq = user.Qq;
            NickName = user.NickName;
            Phone = user.Phone;
            Email = user.Email;
            IsLock = user.IsLock;
            LastLoginIp = user.LastLoginIp;
            LastLoginTime = user.LastLoginTime;
            AddTime = user.AddTime;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

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
        /// 是否锁定
        /// </summary>
        [Display(Name = "状态")]
        public bool IsLock { get; set; }

        /// <summary>
        /// 最近登录时间
        /// </summary>
        [Display(Name = "最近登录时间")]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Display(Name = "添加时间")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 最近登录IP
        /// </summary>
        [Display(Name = "最近登录地址")]
        public string LastLoginIp { get; set; }
    }
}
