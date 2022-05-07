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
    [RoutePrefix("customer")]
    public class CustomerController : Controller
    {
        /// <summary>
        /// Giao diện tìm kiếm
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput model = Session["CUSTOMER_SEARCH"] as PaginationSearchInput;
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
        /// Tìm kiếm và tra về danh sách phân trang khách hàng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomer(input.Page, input.PageSize, input.SearchValue, out rowCount);
            CustomerPaginationResult model = new DomainModel.CustomerPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data,
            };
            Session["CUSTOMER_SEARCH"] = input;
            return View(model);
        }
        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Customer model = new Customer()
            {
                CustomerID = 0,
            };
            ViewBag.Title = "Bổ Sung Khách Hàng";
            return View(model);
        }

        /// <summary>
        /// Sửa khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("edit/{customerID}")]
        public ActionResult Edit(string customerID)
        {
            int customerIDInt = 0;
            try
            {
                customerIDInt = Convert.ToInt32(customerID);
            }
            catch { }
            if (customerIDInt == 0) return RedirectToAction("Index");
            Customer model = CommonDataService.GetCustomer(customerIDInt);
            if (model == null) return RedirectToAction("Index");

            ViewBag.Title = "Chỉnh Sửa Khách Hàng";
            return View("Create", model);
        }
        /// <summary>
        /// Xử lí các tác vụ khách hàng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Customer model)
        {
            //validation model
            if (string.IsNullOrWhiteSpace(model.CustomerName))
                ModelState.AddModelError("CustomerName","Tên khách hàng không được để trống!");
            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống!");
            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "Địa chỉ không được để trống!");
            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "Quốc gia không được để trống!");
            if (string.IsNullOrWhiteSpace(model.City))
                model.City = "";
            if (string.IsNullOrWhiteSpace(model.PostalCode))
                model.PostalCode = "";
            if (!ModelState.IsValid) {
                ViewBag.Title = model.CustomerID == 0 ? "Bổ sung khách hàng" : "Chỉnh sửa khách hàng";
                return View("Create",model);
            }
            Session["CUSTOMER_SEARCH"] = new PaginationSearchInput()
            {
                SearchValue = model.CustomerName,
                PageSize = 10,
                Page = 1,
            };
            // Lưu dữ liệu
            if (model.CustomerID == 0)
            {
                CommonDataService.AddCustomer(model);
                return RedirectToAction("Index");
            } else
            {
                CommonDataService.UpdateCustomer(model);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("delete/{customerID}")]
        public ActionResult Delete(string customerID)
        {
            int customerIDInt = 0;
            try
            {
                customerIDInt = Convert.ToInt32(customerID);
            }
            catch { }
            if (customerIDInt == 0) return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCustomer(customerIDInt);
                return RedirectToAction("Index");
            }
            Customer model = CommonDataService.GetCustomer(customerIDInt);
            if (model == null) return RedirectToAction("Index");
            return View(model);
        }
    }
}