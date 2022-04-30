using SV19T1081001.BusinessLayer;
using SV19T1081001.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV19T1081001.Web.Controllers
{
    [Authorize]
    [RoutePrefix("employee")]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Nhân viên
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
            var data = CommonDataService.ListOfEmployee(pageInt, pageSize, searchValue, out rowCount);
            DomainModel.EmployeePaginationResult model = new DomainModel.EmployeePaginationResult()
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
        /// Thêm nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Employee model = new Employee()
            {
                EmployeeID = 0,
            };
            ViewBag.Title = "Bổ Sung Nhân Viên";
            return View(model);
        }

        /// <summary>
        /// Sửa nhân viên
        /// </summary>
        /// <returns></returns>
        [Route("edit/{employeeID}")]
        public ActionResult Edit(string employeeID)
        {
            int employeeIDInt = 0;
            try
            {
                employeeIDInt = Convert.ToInt32(employeeID);
            }
            catch { }
            if (employeeIDInt == 0) return RedirectToAction("Index");
            Employee model = CommonDataService.GetEmployee(employeeIDInt);
            if (model == null) return RedirectToAction("Index");

            ViewBag.Title = "Chỉnh Sửa Nhân Viên";
            return View("Create", model);
        }
        [HttpPost]
        public ActionResult Save(Employee model, HttpPostedFileBase uploadPhoto, string birthDateString, string oldBirthDateString)
        {
            //validation model
            if (string.IsNullOrWhiteSpace(model.LastName))
                ModelState.AddModelError("LastName", "Họ nhân viên không được để trống!");
            if (string.IsNullOrWhiteSpace(model.FirstName))
                ModelState.AddModelError("FirstName", "Tên nhân viên không được để trống!");
            if (string.IsNullOrWhiteSpace(birthDateString)) { ModelState.AddModelError("BirthDate", "Ngày sinh nhân viên không được để trống!"); }
            else
            {
                try {
                    model.BirthDate = DateTime.ParseExact(oldBirthDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime newBirthday = DateTime.ParseExact(birthDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if ((newBirthday <= (DateTime)SqlDateTime.MinValue) || (newBirthday > DateTime.Now))  ModelState.AddModelError("BirthDate", $"Ngày sinh phải nằm trong khoảng {(DateTime)SqlDateTime.MinValue} đến {DateTime.Now}!");
                    model.BirthDate = newBirthday;
                } catch {
                    ModelState.AddModelError("BirthDate", $"{birthDateString} không hợp lệ");
                }
            }
            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError("Email", "Email nhân viên không được để trống!");
            if (string.IsNullOrWhiteSpace(model.Photo))
                model.Photo = "";
            if (string.IsNullOrWhiteSpace(model.Notes))
                model.Notes = "";

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.EmployeeID == 0 ? "Bổ sung nhân viên" : "Chỉnh sửa nhân viên";
                return View("Create", model);
            }

            try
            {
                if (uploadPhoto != null)
                {
                    string nameFile = DateTime.Now.ToString("yyyyMMddHHmmss") + uploadPhoto.FileName;
                    string path = Path.Combine(Server.MapPath("~/Images/Employees"), Path.GetFileName(nameFile));
                    uploadPhoto.SaveAs(path);
                    model.Photo = "Images/Employees/" + nameFile;
                }
            }
            catch (Exception)
            {
            }


            // Lưu dữ liệu
            if (model.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateEmployee(model);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <returns></returns>
        [Route("delete/{employeeID}")]
        public ActionResult Delete(string employeeID)
        {
            int employeeIDInt = 0;
            try
            {
                employeeIDInt = Convert.ToInt32(employeeID);
            }
            catch { }
            if (employeeIDInt == 0) return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(employeeIDInt);
                return RedirectToAction("Index");
            }
            Employee model = CommonDataService.GetEmployee(employeeIDInt);
            if (model == null) return RedirectToAction("Index");
            return View(model);
        }
    }
}