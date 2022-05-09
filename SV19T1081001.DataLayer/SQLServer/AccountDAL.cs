using SV19T1081001.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DataLayer.SQLServer
{
    public class AccountDAL : _BaseDAL, IAccountDAL
    {
        public AccountDAL(string connectionString) : base(connectionString)
        {
        }

        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"UPDATE Employees
                    SET Password = @NewPassword WHERE Email = @Email AND Password = @OldPassword";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@OldPassword", oldPassword);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account LogIn(string email, string password)
        {
            Account result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"SELECT * FROM Employees WHERE Email = @Email AND Password = @Password";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Account()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        LastName = Convert.ToString(dbReader["LastName"]),
                        FirstName = Convert.ToString(dbReader["FirstName"]),
                        BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                        Email = Convert.ToString(dbReader["Email"]),
                        Notes = Convert.ToString(dbReader["Notes"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        //Password = Convert.ToString(dbReader["Password"]),
                    };
                }

                cn.Close();
            }
            return result;
        }
    }
}
