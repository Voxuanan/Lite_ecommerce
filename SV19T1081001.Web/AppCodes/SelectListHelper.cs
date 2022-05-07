using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV19T1081001.DomainModel;
using SV19T1081001.BusinessLayer;

namespace SV19T1081001.Web
{
    public static class SelectListHelper
    {
        /// <summary>
        /// Danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() {
                Value="",
                Text = "-- Chọn Quốc Gia --"
            });
            foreach (var c in CommonDataService.ListOfCountry())
            {
                list.Add(new SelectListItem()
                {
                    Value = c.CountryName,
                    Text = c.CountryName
                });
            }
            return list;
        }
        /// <summary>
        /// Danh sách loại hàng
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn Loại Hàng --"
            });
            foreach (var c in CommonDataService.ListOfAllCategory())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(c.CategoryID),
                    Text = c.CategoryName
                });
            }
            return list;
        }
        /// <summary>
        /// Danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn Nhà Cung Cấp --"
            });
            foreach (var c in CommonDataService.ListOfAllSupplier())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(c.SupplierID),
                    Text = c.SupplierName
                });
            }
            return list;
        }
    }
}