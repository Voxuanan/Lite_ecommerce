using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DomainModel
{
    /// <summary>
    /// Liệt kê danh sách các quốc gia
    /// </summary>
    public class CategoryPaginationResult : BasePaginationResult
    {
        /// <summary>
        /// Danh sách quốc gia
        /// </summary>
        public List<Category> Data { get; set; }
    }
}
