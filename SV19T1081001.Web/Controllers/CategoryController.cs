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
       /// Loại hàng
       /// </summary>
       /// <returns></returns>
        public ActionResult Index(string page ="1", string searchValue = "")
        {
            int pageInt = 1;
            try
            {
                pageInt = Convert.ToInt32(page);
            }
            catch { }
            int pageSize = 10;
            int rowCount = 0;
            var data = CommonDataService.ListOfCategory(pageInt, pageSize, searchValue, out rowCount);
            DomainModel.CategoryPaginationResult model = new DomainModel.CategoryPaginationResult()
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