using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DomainModel
{
    /// <summary>
    /// Kết quả tìm kiếm mặt hàng
    /// </summary>
    public class ProductPaginationResult : BasePaginationResult
    {
        /// <summary>
        /// Danh sách  mặt hàng
        /// </summary>
        public List<Product> Data { get; set; }
        /// <summary>
        /// Mã nhà cung cấp (dùng cho search)
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// Mã loại hàng (dùng cho search)
        /// </summary>
        public int CategoryID { get; set; }
    }
}
