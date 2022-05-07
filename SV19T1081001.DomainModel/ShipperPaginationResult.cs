using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DomainModel
{
    /// <summary>
    /// Kết quả tìm kiếm người giao hàng
    /// </summary>
    public class ShipperPaginationResult : BasePaginationResult
    {
        /// <summary>
        /// Danh sách người giao hàng
        /// </summary>
        public List<Shipper> Data { get; set; }
    }
}
