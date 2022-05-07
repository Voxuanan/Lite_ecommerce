using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DomainModel
{
    /// <summary>
    /// Kết quả tìm kiếm nhân viên
    /// </summary>
    public class EmployeePaginationResult : BasePaginationResult
    {
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<Employee> Data { get; set; }
    }
}
