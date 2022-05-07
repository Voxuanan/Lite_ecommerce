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
    [RoutePrefix("supplier")]
    public class SupplierController : Controller
    {
        /// <summary>
        /// Giao diện tìm kiếm
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string page = "1", string searchValue = "")
        {
            PaginationSearchInput model = Session["SUPPLIER_SEARCH"] as PaginationSearchInput;
            if (model == null)
            {
                model = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = "",
                };
            }
            return View(model);
        }
        /// <summary>
        /// Tìm kiếm và trả về danh sách phân trang nhà cung cấp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSupplier(input.Page, input.PageSize, input.SearchValue, out rowCount);
            SupplierPaginationResult model = new DomainModel.SupplierPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data,
            };
            Session["SUPPLIER_SEARCH"] = input;
            return View(model);
        }
        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Supplier model = new Supplier()
            {
                SupplierID = 0,
            };
            ViewBag.Title = "Bổ Sung Nhà Cung Cấp";
            return View(model);
        }

        /// <summary>
        /// Sửa nhà cung cấp
        /// </summary>
        /// <returns></returns>
        [Route("edit/{supplierID}")]
        public ActionResult Edit(string supplierID)
        {
            int supplierIDInt = 0;
            try
            {
                supplierIDInt = Convert.ToInt32(supplierID);
            }
            catch { }
            if (supplierIDInt == 0) return RedirectToAction("Index");
            Supplier model = CommonDataService.GetSupplier(supplierIDInt);
            if (model == null) return RedirectToAction("Index");

            ViewBag.Title = "Chỉnh Sửa Nhà Cung Cấp";
            return View("Create",model);
        }
        /// <summary>
        /// Xử lí các tác vụ nhà cung cấp
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Supplier model)
        {
            //validation model
            if (string.IsNullOrWhiteSpace(model.SupplierName))
                ModelState.AddModelError("SupplierName", "Tên nhà cung cấp không được để trống!");
            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống!");
            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "Địa chỉ không được để trống!");
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "Số điện thoại không được để trống!");
            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "Quốc gia không được để trống!");
            if (string.IsNullOrWhiteSpace(model.City))
                model.City = "";
            if (string.IsNullOrWhiteSpace(model.PostalCode))
                model.PostalCode = "";
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Chỉnh sửa nhà cung cấp";
                return View("Create", model);
            }
            Session["SUPPLIER_SEARCH"] = new PaginationSearchInput()
            {
                SearchValue = model.SupplierName,
                PageSize = 10,
                Page = 1,
            };
            // Lưu dữ liệu
            if (model.SupplierID == 0)
            {
                CommonDataService.AddSupplier(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateSupplier(model);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        /// <returns></returns>
        [Route("delete/{supplierID}")]
        public ActionResult Delete(string supplierID)
        {
            int supplierIDInt = 0;
            try
            {
                supplierIDInt = Convert.ToInt32(supplierID);
            }
            catch { }
            if (supplierIDInt == 0) return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(supplierIDInt);
                return RedirectToAction("Index");
            }
            Supplier model = CommonDataService.GetSupplier(supplierIDInt);
            if (model == null) return RedirectToAction("Index");
            return View(model);
        }
    }
}