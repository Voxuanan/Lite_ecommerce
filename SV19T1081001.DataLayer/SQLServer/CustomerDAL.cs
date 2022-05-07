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
    /// <summary>
    /// 
    /// </summary>
    public class CustomerDAL : _BaseDAL, ICommonDAL<Customer>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public CustomerDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Thêm một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Customer data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"INSERT INTO Customers (CustomerName, ContactName, Address, City, PostalCode, Country)
                    VALUES (@customerName,@contactName,@address,@city,@postalCode,@country)
                    SELECT SCOPE_IDENTITY()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@customerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@contactName", data.ContactName);
                cmd.Parameters.AddWithValue("@address", data.Address);
                cmd.Parameters.AddWithValue("@city", data.City);
                cmd.Parameters.AddWithValue("@postalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@country", data.Country);

                result  = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Số lượng kết quả search được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            int count = 0;
            if (searchValue != "") searchValue = $"%{searchValue}%";
            using (SqlConnection cn = OpenConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"
                    SELECT Count(CustomerID)
                    FROM Customers
                    WHERE (@searchValue = N'') OR ((CustomerName LIKE @searchValue) OR (Address LIKE @searchValue) OR (ContactName LIKE @searchValue))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
                return count;
        }
        /// <summary>
        /// Xóa một khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool Delete(int customerID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"DELETE FROM Customers WHERE CustomerID = @customerID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@customerID", customerID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy thông tin một khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer Get(int customerID)
        {
            Customer result = new Customer();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"SELECT * FROM Customers WHERE CustomerID = @customerID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@customerID", customerID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Customer()
                    {
                        CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                        CustomerName = Convert.ToString(dbReader["CustomerName"]),
                        City = Convert.ToString(dbReader["City"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Country = Convert.ToString(dbReader["Country"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Address = Convert.ToString(dbReader["Address"]),
                    };
                }

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Kiểm tra xem khách hàng có được sử dụng hay chưa
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool InUsed(int customerID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"SELECT CASE WHEN EXISTS(SELECT * FROM Orders WHERE CustomerID = @customerID) THEN 1 ELSE 0 END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@customerID", customerID);
                result = Convert.ToInt32(cmd.ExecuteScalar()) == 1;

                cn.Close();
            }
             return result;
        }
        /// <summary>
        /// Lây danh sách khách hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IList<Customer> List(int page, int pageSize, string searchValue)
        {
            List<Customer> data = new List<Customer>();
            if (searchValue != "") searchValue = $"%{searchValue}%";
            using (SqlConnection cn = OpenConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"
                    SELECT *
                    FROM (
                        SELECT *, ROW_NUMBER() OVER (ORDER BY CustomerName) AS RowNum
                        FROM Customers
	                    WHERE (@searchValue = N'') OR ((CustomerName LIKE @searchValue) OR (Address LIKE @searchValue) OR (ContactName LIKE @searchValue))
                    ) AS MyDerivedTable
                    WHERE (@pageSize = 0) OR MyDerivedTable.RowNum BETWEEN (@page-1)*@pageSize+1 AND @page*@pageSize";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Customer()
                    {
                        CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                        CustomerName = Convert.ToString(dbReader["CustomerName"]),
                        City = Convert.ToString(dbReader["City"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Country = Convert.ToString(dbReader["Country"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Address = Convert.ToString(dbReader["Address"]),
                    });
                }

                cn.Close();
            }

            return data;
        }
        /// <summary>
        /// Chỉnh sửa khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Customer data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"UPDATE Customers SET CustomerName = @customerName, ContactName = @contactName, Address = @address, City = @city, PostalCode = @postalCode, Country = @country
                    WHERE CustomerID = @customerID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@customerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@customerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@contactName", data.ContactName);
                cmd.Parameters.AddWithValue("@address", data.Address);
                cmd.Parameters.AddWithValue("@city", data.City);
                cmd.Parameters.AddWithValue("@postalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@country", data.Country);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
