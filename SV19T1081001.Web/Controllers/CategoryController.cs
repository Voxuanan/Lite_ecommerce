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
    [RoutePrefix("category")]
    public class CategoryController : Controller
    {
       /// <summary>
       /// Giao diện tìm kiếm
       /// </summary>
       /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput model = Session["CATEGORY_SEARCH"] as PaginationSearchInput;
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
        /// Tìm kiếm và trả về danh sách phân trang loại hàng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCategory(input.Page, input.PageSize, input.SearchValue, out rowCount);
            CategoryPaginationResult model = new DomainModel.CategoryPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data,
            };
            Session["CATEGORY_SEARCH"] = input;
            return View(model);
        }
        /// <summary>
        /// Thêm loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Category model = new Category()
            {
                CategoryID = 0,
            };
            ViewBag.Title = "Bổ Sung Loại Hàng";
            return View(model);
        }

        /// <summary>
        /// Sửa loại hàng
        /// </summary>
        /// <returns></returns>
        [Route("edit/{categoryID}")]
        public ActionResult Edit(string categoryID)
        {
            int categoryIDInt = 0;
            try
            {
                categoryIDInt = Convert.ToInt32(categoryID);
            }
            catch { }
            if (categoryIDInt == 0 ) return RedirectToAction("Index");
            Category model = CommonDataService.GetCategory(categoryIDInt);
            if (model == null) return RedirectToAction("Index");

            ViewBag.Title = "Chỉnh Sửa Loại Hàng";
            return View("Create", model);
        }
        /// <summary>
        /// Xử lí các tác vụ loại hàng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Category model)
        {
            //validation model
            if (string.IsNullOrWhiteSpace(model.CategoryName))
                ModelState.AddModelError("CategoryName", "Tên khách hàng không được để trống!");
            if (string.IsNullOrWhiteSpace(model.Description))
                model.Description = "";
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.CategoryID == 0 ? "Bổ sung loại hàng" : "Chỉnh sửa loại hàng";
                return View("Create", model);
            }
            Session["CATEGORY_SEARCH"] = new PaginationSearchInput()
            {
                SearchValue = model.CategoryName,
                PageSize = 10,
                Page = 1,   
            };
            if (model.CategoryID == 0)
            {
                CommonDataService.AddCategory(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateCategory(model);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Xóa loại hàng
        /// </summary>
        /// <returns></returns>
        [Route("delete/{categoryID}")]
        public ActionResult Delete(string categoryID)
        {
            int categoryIDInt = 0;
            try
            {
                categoryIDInt = Convert.ToInt32(categoryID);
            }
            catch { }
            if (categoryIDInt == 0) return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCategory(categoryIDInt);
                return RedirectToAction("Index");
            }
            Category model = CommonDataService.GetCategory(categoryIDInt);
            if (model == null) return RedirectToAction("Index");
            return View(model);
       
        }
    }
}