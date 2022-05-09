using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DomainModel
{
    public class Account
    {
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// Họ
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Link ảnh
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
    }
}
