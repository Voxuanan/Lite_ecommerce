using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DomainModel
{
    /// <summary>
    /// Lớp cơ sở (lớp cha) cho các lớp lưu trũ các dữ liệu
    /// liên quan đến phân trang
    /// </summary>
    public abstract class BasePaginationResult
    {
        /// <summary>
        /// Trang cần xem
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Số dòng trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Giá trị tìm kiếm
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// Tổng số dòng
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// Số trang hiện có
        /// </summary>
        public int PageCount { 
            get {
                if (PageSize == 0) return 1;
                int p = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                {
                    p++;
                }
                return p;
            } 
        }
    }
}
