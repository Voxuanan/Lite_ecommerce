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
    }
}
