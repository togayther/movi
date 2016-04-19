using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Movi.Common;
using Movi.Model;
using Movi.Model.Enums;
using Movi.ViewModel;

namespace Movi.Controllers
{
    public class AdminController : BaseController
    {
        #region init
        /// <summary>
        /// 初始化系统超级管理员
        /// </summary>
        static AdminController()
        {
            var hasUserExists = UserService.GetAll().Any();
            if (!hasUserExists)
            {
                var salt = StringHelper.GetGuidString();
                var superManager = new User
                {
                    Id = StringHelper.GetGuidString(),
                    Avatar = "/Upload/Avatars/avatar.jpg",
                    Name = "admin",
                    Password = CryptoHelper.MD5Encrypt("admin" + salt),
                    NickName = "一天到晚游泳的鱼",
                    Email = "bigbrotherbiger@gmail.com",
                    Phone = "13540312451",
                    Qq = "353066897",
                    AddTime = DateTime.Now,
                    IsSuperUser = true,
                    IsLock = false,
                    Salt = salt
                };

                UserService.Insert(superManager);
            }
        }
        #endregion

        /// <summary>
        /// 控制面板
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || LoginUser == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            var dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.LoginInfo = new LoginInfoModel()
            {
                ThisLoginIp = HttpHelper.GetClientIPAddress(),
                LastLoginIp = LoginUser.LastLoginIp,
                LastLoginTime = LoginUser.LastLoginTime
            };
            dashboardViewModel.SiteInfo = new SiteInfoModel(Config);
            dashboardViewModel.GrabInfo = new GrabInfoModel()
            {
                MovieGrabCount = MediaService.GetAll().Count(c => c.Type == MediaType.Movie && (c.AddTime > LoginUser.LastLoginTime)),
                CartoonGrabCount = MediaService.GetAll().Count(c => c.Type == MediaType.Cartoon && (c.AddTime > LoginUser.LastLoginTime)),
                TeleplayGrabCount = MediaService.GetAll().Count(c => c.Type == MediaType.Teleplay && (c.AddTime > LoginUser.LastLoginTime))
            };
            return View(dashboardViewModel);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserLoginViewModel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userName = ValidateHelper.SqlFilter(user.Name);
                    var userPwd = ValidateHelper.SqlFilter(user.Password);

                    var loginUser = UserService.CheckLogin(userName, userPwd);
                    if (loginUser == null)
                    {
                        ModelState.AddModelError("Global", "登录失败，用户名或密码错误！");
                    }
                    else
                    {
                        //如果该用户已锁定
                        if (loginUser.IsLock)
                        {
                            ModelState.AddModelError("Global", "该用户已锁定，登录失败！");
                        }
                        else
                        {
                            //如果是超级管理员
                            if (loginUser.IsSuperUser)
                            {

                            }

                            //设置用户登录状态
                            FormsAuthentication.SetAuthCookie(loginUser.Id, true);

                            //缓存登录用户信息
                            CacheHelper.SetCache(Constant.CacheKey.LoginUserInfoCacheKey + loginUser.Id, loginUser.Clone());

                            //更新用户登录信息
                            loginUser.LastLoginTime = DateTime.Now;
                            loginUser.LastLoginIp = HttpHelper.GetClientIPAddress();

                            UserService.Update(loginUser);

                            if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Global", "内部服务器错误，登录失败！Error:" + e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("Global", "数据验证错误，登录失败！");
            }

            return View(user);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            //移除缓存的登录用户信息
            CacheHelper.RemoveAllCache(Constant.CacheKey.LoginUserInfoCacheKey + User.Identity.Name);

            //用户注销
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Admin");
        }

        /// <summary>
        /// 用户锁定
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Lock()
        {
            /*
             *  存储当前锁定用户的编号，
             *  在调用 FormsAuthentication.SignOut() 后，
             *  仍然可以根据存储的用户编号，从缓存中读取到该用户的信息。
             *  注：用户信息缓存键为 Constant.CacheKey.LoginUserInfoCacheKey + userid
             */

            var identityUser = User.Identity.Name;
            if (String.IsNullOrEmpty(identityUser))
            {
                identityUser = CookieHelper.GetCookieValue("LOGIN_USER_ID");
            }
            else
            {
                CookieHelper.SetCookie("LOGIN_USER_ID", identityUser);
            }

            var user = CacheHelper.GetCache(Constant.CacheKey.LoginUserInfoCacheKey + identityUser) as User;
            if (user == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            FormsAuthentication.SignOut();

            var loginUser = new UserLoginViewModel(user);
            return View(loginUser);
        }

        /// <summary>
        /// 用户锁定
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Lock(UserLoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userName = ValidateHelper.SqlFilter(user.Name);
                    var userPwd = ValidateHelper.SqlFilter(user.Password);

                    var loginUser = UserService.CheckLogin(userName, userPwd);
                    if (loginUser == null)
                    {
                        ModelState.AddModelError("Global", "解锁失败，密码输入错误！");
                    }
                    else
                    {
                        //如果该用户已锁定
                        if (loginUser.IsLock)
                        {
                            ModelState.AddModelError("Global", "该用户已锁定，解锁失败！");
                        }
                        else
                        {
                            //如果是超级管理员
                            if (loginUser.IsSuperUser)
                            {

                            }

                            FormsAuthentication.SetAuthCookie(loginUser.Id, true);

                            return RedirectToAction("Index", "Admin");
                        }
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Global", "内部服务器错误，解锁失败！Error:" + e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("Global", "数据验证错误，解锁失败！");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Findpwd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Findpwd(UserLoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userName = ValidateHelper.SqlFilter(user.Name);
                    var userEmail = ValidateHelper.SqlFilter(user.Email);

                    var lostUser =
                        UserService.GetAll().FirstOrDefault(c => c.Name.Equals(userName) && c.Email.Equals(userEmail));


                    if (lostUser == null)
                    {
                        ModelState.AddModelError("Global", "未查询到相关用户，找回密码失败！");
                    }
                    else
                    {
                        //如果该用户已锁定
                        if (lostUser.IsLock)
                        {
                            ModelState.AddModelError("Global", "该用户已锁定，找回密码失败！");
                        }
                        else
                        {
                            var newpwd = StringHelper.GetRandomString();
                            var subject = String.Format("{0} - 密码找回", Config.SiteName);
                            var body = String.Format("系统已重置登录密码为：{0}，请及时登录系统修改该密码.",newpwd);
                            var mailSender = GetMailSender(subject, body, userEmail);

                            mailSender.Send();

                            TempData["Success"] = "密码已重置，请登录邮箱查看新的登录密码！";
                        }
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Global", "内部服务器错误，找回密码失败！Error:" + e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("Global", "数据验证错误，找回密码失败！");
            }

            return View(user);
        }
    }
}
