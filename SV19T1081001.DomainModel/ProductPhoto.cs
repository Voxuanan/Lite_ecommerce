using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DomainModel
{
    /// <summary>
    /// Ảnh phụ mặt hàng
    /// </summary>
    public class ProductPhoto
    {
        /// <summary>
        /// Mã ảnh
        /// </summary>
        public int PhotoID { get; set; }
        /// <summary>
        /// Mã mặt hàng
        /// </summary>
        public int  ProductID { get; set; }
        /// <summary>
        /// Link ảnh
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Ẩn ảnh
        /// </summary>
        public bool IsHidden { get; set; }
    }
}
