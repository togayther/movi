using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Practices.ServiceLocation;
using Movi.Common;
using Movi.IService;
using Movi.Model;
using Movi.Model.Enums;
using Movi.ViewModel;
using Webdiyer.WebControls.Mvc;

namespace Movi.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var users = UserService.GetAll()
                .Where(c=>!c.Id.Equals(User.Identity.Name))
                .OrderByDescending(c => c.LastLoginTime).ToList();

            ViewData["Users"] = users;

            return View("~/Views/Admin/User/List.cshtml");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("~/Views/Admin/User/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(HttpPostedFileBase file, User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    InitUserDefault(user);

                    SaveAvatarData(user);


                    UserService.Insert(user);

                    return RedirectToAction("Index", "User");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Global", "内部服务器错误，添加失败！Error:" + e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("Global", "数据验证错误，添加失败！");
            }

            return View("~/Views/Admin/User/Add.cshtml", user);
        }

        [HttpGet]
        public ActionResult Center()
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View("~/Views/Admin/User/Center.cshtml",LoginUser);
        }

        [HttpPost]
        public ActionResult Center(HttpPostedFileBase file, User editUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = UserService.Find(c => c.Id.Equals(editUser.Id));
                    if (user != null)
                    {
                        user.Name = editUser.Name;
                        user.NickName = editUser.NickName;
                        user.Email = editUser.Email;
                        user.Qq = editUser.Qq;
                        user.Phone = editUser.Phone;

                        if (!editUser.Password.Equals(user.Password))
                        {
                            user.Salt = StringHelper.GetGuidString();
                            user.Password = CryptoHelper.MD5Encrypt(editUser.Password+user.Salt);
                        }

                        SaveAvatarData(user);
                        UserService.Update(user);
                        editUser.Avatar = user.Avatar;
                        TempData["Success"] = "恭喜你，更新个人信息成功！";

                        //更新成功后重新登录
                        FormsAuthentication.SignOut();
                    }
                    else
                    {
                        ModelState.AddModelError("Global", "未查询到待更新的用户信息，更新失败！");
                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Global", "内部服务器错误，更新失败！Error:" + e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("Global", "数据验证错误，更新失败！");
            }

            return View("~/Views/Admin/User/Center.cshtml", editUser);
        }

        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string uid)
        {
            UserEditViewModel editUser = null;
            if (!String.IsNullOrEmpty(uid))
            {
                var user = UserService.Find(c => c.Id.Equals(uid));
                editUser = new UserEditViewModel(user);
            }
            return View("~/Views/Admin/User/Edit.cshtml", editUser);
        }

        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <param name="editUser"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(UserEditViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = UserService.Find(c => c.Id.Equals(editUser.Id));
                    if (user != null)
                    {
                        user.NickName = editUser.NickName;
                        user.Email = editUser.Email;
                        user.Qq = editUser.Qq;
                        user.Phone = editUser.Phone;
                        user.IsLock = editUser.IsLock;

                        SaveAvatarData(user);

                        UserService.Update(user);
                        editUser.Avatar = user.Avatar;
                        TempData["Success"] = "恭喜你，更新成功！";

                        //更新缓存的用户信息
                        UpdateCacheUserInfo(user);
                    }
                    else
                    {
                        ModelState.AddModelError("Global", "未查询到待更新的用户信息，更新失败！");
                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Global", "内部服务器错误，更新失败！Error:" + e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("Global", "数据验证错误，更新失败！");
            }

            return View("~/Views/Admin/User/Edit.cshtml", editUser);
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Lock(string uid)
        {
            if (!String.IsNullOrEmpty(uid))
            {
                var user = UserService.Find(c => c.Id.Equals(uid));
                if (user != null)
                {
                }
            }
            return RedirectToAction("Index", "User");
        }

        #region Helper

        /// <summary>
        /// 初始化用户默认数据
        /// </summary>
        /// <param name="user"></param>
        private void InitUserDefault(User user)
        {
            user.AddTime = DateTime.Now;
            if (String.IsNullOrEmpty(user.Id))
            {
                user.Id = StringHelper.GetGuidString();
            }
            if (String.IsNullOrEmpty(user.Salt))
            {
                user.Salt = StringHelper.GetGuidString();
            }
            if (String.IsNullOrEmpty(user.Avatar))
            {
                user.Avatar = Config.DefaultAvatar;
            }
            user.Password = CryptoHelper.MD5Encrypt(user.Password + user.Salt);
        }

        /// <summary>
        /// 接收用户头像数据
        /// </summary>
        /// <param name="user"></param>
        private void SaveAvatarData(User user)
        {
            var avatarFile = Request.Files["AvatarFile"];
            if (avatarFile != null && avatarFile.ContentLength > 0)
            {
                if (avatarFile.ContentLength <= Config.ImgUploadSize * 1024)
                {
                    var avatarName = avatarFile.FileName;
                    var avatarExt = Path.GetExtension(avatarName);

                    if (!String.IsNullOrEmpty(avatarExt)
                        && Config.ImgExtensions.Contains(avatarExt))
                    {
                        //保存原图
                        var savePath = Path.Combine(Server.MapPath(Config.AvatarFilePath), avatarName);
                        avatarFile.SaveAs(savePath);

                        //缩略图路径
                        var thumbPath = Path.Combine(Server.MapPath(Config.AvatarFilePath), user.Id + avatarExt);

                        //生成头像缩略图
                        ImageHelper.MakeThumbnailImage(savePath, thumbPath, Config.AvatarWidth, Config.AvatarHeight, "HW");

                        user.Avatar = Path.Combine(Config.AvatarFilePath, user.Id + avatarExt);
                    }
                    else
                    {
                        throw new ArgumentException("上传文件类型错误");
                    }
                }
                else
                {
                    throw new ArgumentException("上传文件大小超出限制");
                }
            }
        }

        /// <summary>
        /// 更新缓存的用户信息
        /// </summary>
        /// <param name="user"></param>
        private void UpdateCacheUserInfo(User user)
        {
            //清除缓存的登录用户信息
            if (User.Identity.Name.Equals(user.Id))
            {
                var cacheLoginUser = CacheHelper.GetCache(Constant.CacheKey.LoginUserInfoCacheKey) as User;
                cacheLoginUser.NickName = user.NickName;
                cacheLoginUser.Email = user.Email;
                cacheLoginUser.Qq = user.Qq;
                cacheLoginUser.Phone = user.Phone;
                cacheLoginUser.Avatar = user.Avatar;
                CacheHelper.SetCache(Constant.CacheKey.LoginUserInfoCacheKey, cacheLoginUser);
            }
        }

        #endregion
    }
}
