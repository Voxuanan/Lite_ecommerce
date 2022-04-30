using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SV19T1081001.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// Giao diện đăng nhập hệ thống
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // TODO: Thay đổi code đề kiểm tra đúng tài khoản
            if (username == "admin@gmail.com" && password == "123")
            {
                // Ghi cookie ghi nhận phiên đăng nhập
                FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("Index", "Home");
            } else
            {
                ViewBag.UserName = username;
                ViewBag.Message = "Đăng nhập thất bại";
                return View();
            }
        }
        /// <summary>
        /// Đổi mặt khẩu
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }
        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}