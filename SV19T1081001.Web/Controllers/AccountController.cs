using SV19T1081001.BusinessLayer;
using SV19T1081001.DomainModel;
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
            Account account = null;
            account = CommonDataService.LogIn(username, password);
            if (account != null)
            {
                // Ghi cookie ghi nhận phiên đăng nhập
                FormsAuthentication.SetAuthCookie(username, false);
                Session["ACCOUNT"] = account;
                return RedirectToAction("Index", "Home");
            }
            else
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
        public ActionResult ChangePassword(string password, string newPassword, string confirmPassword)
        {
            Account model = Session["ACCOUNT"] as Account;
            if (Request.HttpMethod == "POST")
            {
                if (string.IsNullOrWhiteSpace(password))
                    ModelState.AddModelError("Password", "Mật khẩu cũ không được để trống!");
                if (string.IsNullOrWhiteSpace(newPassword))
                    ModelState.AddModelError("NewPassword", "Mật khẩu mới không được để trống!");
                if (newPassword != confirmPassword)
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu nhập lại phải giống mật khẩu mới!");
                if (!ModelState.IsValid) {
                    ViewBag.Password = password;
                    ViewBag.NewPassword = newPassword;
                    ViewBag.ConfirmPassword = confirmPassword;
                    return View(model); 
                }
                bool hasChanged = CommonDataService.ChangePassword(model.Email, password, newPassword);
                if (!hasChanged)
                {
                    ModelState.AddModelError("Password", "Mật khẩu cũ không đúng!");
                    return View(model);
                }
                model.Password = newPassword;
                Session["ACCOUNT"] = model;
                return Redirect("~/");
            }
            return View(model);
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