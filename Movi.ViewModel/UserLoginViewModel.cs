using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movi.Model;

namespace Movi.ViewModel
{
    public class UserLoginViewModel
    {
        public UserLoginViewModel()
        {
        }

        public UserLoginViewModel(User user)
        {
            Name = user.Name;
            Avatar = user.Avatar;
            Email = user.Email;
        }

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
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 伊妹儿
        /// </summary>
        public string Email { get; set; }
    }
}
