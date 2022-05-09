using SV19T1081001.BusinessLayer;
using SV19T1081001.DomainModel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV19T1081001.Web.Controllers
{
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        /// <summary>
        /// Giao diện tìm kiếm
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int categoryID = 0, int supplierID = 0)
        {

            PaginationSearchInputForProduct model = Session["PRODUCT_SEARCH"] as PaginationSearchInputForProduct;
            if (model == null)
            {
                model = new PaginationSearchInputForProduct()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = "",
                    CategoryID = 0,
                    SupplierID = 0,
                };
            }
            if (supplierID != 0)
            {
                model.SupplierID = supplierID;
                model.SearchValue = "";
                model.CategoryID = 0;
            }
            if (categoryID != 0)
            {
                model.SupplierID = 0;
                model.SearchValue = "";
                model.CategoryID = categoryID;
            }
            return View(model);
        }
        /// <summary>
        /// Tìm kiếm và trả về danh sách phân trang mặt hàng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInputForProduct input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfProduct(input.Page, input.PageSize, input.SearchValue, input.SupplierID, input.CategoryID, out rowCount);
            ProductPaginationResult model = new DomainModel.ProductPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data,
            };
            Session["PRODUCT_SEARCH"] = input;
            return View(model);
        }
        /// <summary>
        /// Thêm mặt hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Product model = new Product()
            {
                ProductID = 0,
            };
            return View(model);
        }
        /// <summary>
        /// Chỉnh sửa mặt hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("edit/{productID}")]
        public ActionResult Edit(int productID)
        {
            int productIDInt = 0;
            try
            {
                productIDInt = Convert.ToInt32(productID);
            }
            catch { }
            if (productIDInt == 0) return RedirectToAction("Index");

            Product model = CommonDataService.GetProduct(productIDInt);
            if (model == null) return RedirectToAction("Index");
            return View(model);
        }
        /// <summary>
        /// Xử lí các tác vụ mặt hàng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Product model, HttpPostedFileBase uploadPhoto)
        {
            //validation model
            if (string.IsNullOrWhiteSpace(model.ProductName))
                ModelState.AddModelError("ProductName", "Tên sản phẩm không được để trống!");
            if (string.IsNullOrWhiteSpace(model.Unit))
                ModelState.AddModelError("Unit", "Đơn vị tính không được để trống!");
            if (string.IsNullOrWhiteSpace(model.Photo)) model.Photo = " ";
            if (!ModelState.IsValid)
            {
                if (model.ProductID == 0) return View("Create", model);
                else return View("Edit", model);
            }

            try
            {
                if (uploadPhoto != null)
                {
                    string nameFile = DateTime.Now.ToString("yyyyMMddHHmmss") + uploadPhoto.FileName;
                    string path = Path.Combine(Server.MapPath("~/Images/Products"), Path.GetFileName(nameFile));
                    uploadPhoto.SaveAs(path);
                    model.Photo = "Images/Products/" + nameFile;
                }
            }
            catch (Exception)
            {
            }
            Session["PRODUCT_SEARCH"] = new PaginationSearchInputForProduct()
            {
                SearchValue = model.ProductName,
                PageSize = 10,
                Page = 1,
                CategoryID = model.CategoryID,
                SupplierID = model.SupplierID,
               
            };


            // Lưu dữ liệu
            if (model.ProductID == 0)
            {
                CommonDataService.AddProduct(model);
            }
            else
            {
                CommonDataService.UpdateProduct(model);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("delete/{productID}")]
        public ActionResult Delete(string productID)
        {
            int productIDInt = 0;
            try
            {
                productIDInt = Convert.ToInt32(productID);
            }
            catch { }
            if (productIDInt == 0) return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteProduct(productIDInt);
                return RedirectToAction("Index");
            }
            Product model = CommonDataService.GetProduct(productIDInt);
            if (model == null) return RedirectToAction("Index");
            return View(model);
        }
        /// <summary>
        /// Điều hướng ảnh mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method}/{productID}/{photoID?}")]
        public ActionResult Photo(string method, string productID, string photoID)
        {
            int productIDInt = 0;
            try
            {
                productIDInt = Convert.ToInt32(productID);
            }
            catch { }
            if (productIDInt == 0) return RedirectToAction("Index");
            int photoIDInt = 0;
            try
            {
                photoIDInt = Convert.ToInt32(photoID);
            }
            catch { }
            if (photoIDInt == 0 && ((method == "edit") || (method == "delete"))) return RedirectToAction("Index");

            switch (method)
            {
                case "add":
                    ProductPhoto model = new ProductPhoto()
                    {
                        PhotoID = 0,
                        ProductID = productIDInt,
                        IsHidden = false,
                    };
                    ViewBag.Title = "Bổ sung ảnh";
                    return View(model);


                case "edit":
                   
                    ProductPhoto model1 = CommonDataService.GetProductPhoto(photoIDInt);
                    if (model1 == null) return RedirectToAction("Index");
                    ViewBag.Title = "Thay đổi ảnh";
                    return View(model1);

                case "delete":
                    CommonDataService.DeleteProductPhoto(photoIDInt);
                    return RedirectToAction("Edit", new { productID = productID });


                default:
                    return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// Xử lí các tác vụ ảnh mặt hàng
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SavePhoto(ProductPhoto model, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(model.Description)) model.Description = " ";
            if (uploadPhoto == null && model.Photo == null) ModelState.AddModelError("Photo", "Hình ảnh không được để trống!");
            //validation model
            if (!ModelState.IsValid)
            {
                if (model.PhotoID == 0)
                {
                    ViewBag.Title = "Bổ sung ảnh";
                }
                else
                {
                    ViewBag.Title = "Thay đổi ảnh";
                }
                return View("Photo", model);
            }


            try
            {
                if (uploadPhoto != null)
                {
                    string nameFile = DateTime.Now.ToString("yyyyMMddHHmmss") + uploadPhoto.FileName;
                    string path = Path.Combine(Server.MapPath("~/Images/ProductPhotos"), Path.GetFileName(nameFile));
                    uploadPhoto.SaveAs(path);
                    model.Photo = "Images/ProductPhotos/" + nameFile;
                }
            }
            catch (Exception)
            {
            }


            // Lưu dữ liệu
            if (model.PhotoID == 0)
            {
                CommonDataService.AddProductPhoto(model);
            }
            else
            {
                CommonDataService.UpdateProductPhoto(model);
            }
            return Redirect($"/product/edit/{model.ProductID}");
        }
        /// <summary>
        /// Diều hướng thuộc tính mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method, string productID, string attributeID)
        {
            int productIDInt = 0;
            try
            {
                productIDInt = Convert.ToInt32(productID);
            }
            catch { }
            if (productIDInt == 0) return RedirectToAction("Index");
            int attributeIDInt = 0;
            try
            {
                attributeIDInt = Convert.ToInt32(attributeID);
            }
            catch { }
            if (attributeIDInt == 0 && ((method == "edit") || (method == "delete"))) return RedirectToAction("Index");

            switch (method)
            {
                case "add":
                    ProductAttribute model = new ProductAttribute()
                    {
                        AttributeID = 0,
                        ProductID = productIDInt,
                    };
                    ViewBag.Title = "Bổ sung thuộc tính";
                    return View(model);


                case "edit":
                 
                    ProductAttribute model1 = CommonDataService.GetProductAttribute(attributeIDInt);
                    if (model1 == null) return RedirectToAction("Index");
                    ViewBag.Title = "Thay đổi thuộc tính";
                    return View(model1);


                case "delete":
                    CommonDataService.DeleteProductAttribute(attributeIDInt);
                    return RedirectToAction("Edit", new { productID = productID });


                default:
                    return RedirectToAction("Index");

            }
        }
        /// <summary>
        /// Xử lí các tác vụ thuộc tính mặt hàng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveAttribute(ProductAttribute model)
        {
            if (string.IsNullOrWhiteSpace(model.AttributeName)) ModelState.AddModelError("AttributeName", "Tên thuộc tính không được để trống!");
            if (string.IsNullOrWhiteSpace(model.AttributeValue)) ModelState.AddModelError("AttributeValue", "Giá trị thuộc tính không được để trống!");
            //validation model
            if (!ModelState.IsValid)
            {
                if (model.AttributeID == 0)
                {
                    ViewBag.Title = "Bổ sung thuộc tính";
                } else
                {
                    ViewBag.Title = "Thay đổi thuộc tính";
                }
                return View("Attribute", model);
            }


            // Lưu dữ liệu
            if (model.AttributeID == 0)
            {
                CommonDataService.AddProductAttribute(model);
            }
            else
            {
                CommonDataService.UpdateProductAttribute(model);
            }
            return Redirect($"/product/edit/{model.ProductID}");
        }
    }
}