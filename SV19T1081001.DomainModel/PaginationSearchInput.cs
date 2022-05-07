using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DomainModel
{
    /// <summary>
    /// Dữ liệu cho tìm kiếm, phân trang
    /// </summary>
    public class PaginationSearchInput
    {
        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Số cột dữ liệu trên một trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string SearchValue { get; set; }
    }
}
