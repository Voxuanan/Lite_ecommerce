using SV19T1081001.BusinessLayer;
using SV19T1081001.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV19T1081001.Web.Controllers
{
    [Authorize]
    [RoutePrefix("shipper")]
    public class ShipperController : Controller
    {
        /// <summary>
        /// Tìm kiếm hiển thị danh sách người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string page = "1", string searchValue = "")
        {
            int pageInt = 1;
            try
            {
                pageInt = Convert.ToInt32(page);
            }
            catch { }
            int pageSize = 10;
            int rowCount = 0;
            var data = CommonDataService.ListOfShipper(pageInt, pageSize, searchValue, out rowCount);
            DomainModel.ShipperPaginationResult model = new DomainModel.ShipperPaginationResult()
            {
                Page = pageInt,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = data,
            };
            return View(model);
        }
        /// <summary>
        /// Thêm người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Shipper model = new Shipper()
            {
                ShipperID = 0,
            };
            ViewBag.Title = "Bổ Sung Người Giao Hàng";
            return View(model);
        }

        /// <summary>
        /// Sửa người giao hàng
        /// </summary>
        /// <returns></returns>
        [Route("edit/{shipperID}")]
        public ActionResult Edit(string shipperID)
        {
            int shipperIDInt = 0;
            try
            {
                shipperIDInt = Convert.ToInt32(shipperID);
            }
            catch { }
            if (shipperIDInt == 0) return RedirectToAction("Index");
            Shipper model = CommonDataService.GetShipper(shipperIDInt);
            if (model == null) return RedirectToAction("Index");
            ViewBag.Title = "Chỉnh Sửa Người Giao Hàng";
            return View("Create",model);
        }
        [HttpPost]
        public ActionResult Save(Shipper model)
        {
            //validation model
            if (string.IsNullOrWhiteSpace(model.ShipperName))
                ModelState.AddModelError("ShipperName", "Tên người giao hàng không được để trống!");
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "Số điện thoại người giao hàng không được để trống!");
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.ShipperID == 0 ? "Bổ sung khách hàng" : "Chỉnh sửa khách hàng";
                return View("Create", model);
            }

            if (model.ShipperID == 0)
            {
                CommonDataService.AddShipper(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateShipper(model);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Xóa người giao hàng
        /// </summary>
        /// <returns></returns>
        [Route("delete/{shipperID}")]
        public ActionResult Delete(string shipperID)
        {
            int shipperIDInt = 0;
            try
            {
                shipperIDInt = Convert.ToInt32(shipperID);
            }
            catch { }
            if (shipperIDInt == 0) return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteShipper(shipperIDInt);
                return RedirectToAction("Index");
            }
            Shipper model = CommonDataService.GetShipper(shipperIDInt);
            if (model == null) return RedirectToAction("Index");
            return View(model);
        }
    }
}