using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV19T1081001.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        /// <summary>
        /// Tra cứu đơn hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Lập đơn hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Lập Đơn Hàng";
            return View();
        }

        /// <summary>
        /// Sửa đơn hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            ViewBag.Title = "Chỉnh Sửa Đơn Hàng";
            return View("Create");
        }

        /// <summary>
        /// Xóa đơn hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            return View();
        }
    }
}