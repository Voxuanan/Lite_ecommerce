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
    public class SupplierDAL : _BaseDAL, ICommonDAL<Supplier>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public SupplierDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Thêm một nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Supplier data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"INSERT INTO Suppliers (SupplierName, ContactName, Address, City, PostalCode, Country,Phone)
                    VALUES (@supplierName,@contactName,@address,@city,@postalCode,@country,@Phone)
                    SELECT SCOPE_IDENTITY()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@supplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@contactName", data.ContactName);
                cmd.Parameters.AddWithValue("@address", data.Address);
                cmd.Parameters.AddWithValue("@city", data.City);
                cmd.Parameters.AddWithValue("@postalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                result = Convert.ToInt32(cmd.ExecuteScalar());

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
                    SELECT Count(SupplierID)
                    FROM Suppliers
                    WHERE (@searchValue = N'') OR ((SupplierName LIKE @searchValue) OR (Address LIKE @searchValue) OR (ContactName LIKE @searchValue) OR (Phone LIKE @searchValue))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return count;
        }
        /// <summary>
        /// Xóa một nhà cung cấp có sẵn
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public bool Delete(int supplierID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"DELETE FROM Suppliers WHERE SupplierID = @SupplierID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy dữ liệu một nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public Supplier Get(int supplierID)
        {
            Supplier result = new Supplier();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"SELECT * FROM Suppliers WHERE SupplierID = @SupplierID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Supplier()
                    {
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        SupplierName = Convert.ToString(dbReader["SupplierName"]),
                        City = Convert.ToString(dbReader["City"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Country = Convert.ToString(dbReader["Country"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Address = Convert.ToString(dbReader["Address"]),
                        Phone = Convert.ToString(dbReader["Phone"])
                    };
                }

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Kiểm tra nhà cung cấp có được sử dụng hay chưa
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public bool InUsed(int supplierID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"SELECT CASE WHEN EXISTS(SELECT * FROM Products WHERE SupplierID = @SupplierID) THEN 1 ELSE 0 END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                result = Convert.ToInt32(cmd.ExecuteScalar()) == 1;

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy danh sách các nhà cung cấp
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IList<Supplier> List(int page, int pageSize, string searchValue)
        {
            List<Supplier> data = new List<Supplier>();
            if (searchValue != "") searchValue = $"%{searchValue}%";
            using (SqlConnection cn = OpenConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"
                    SELECT *
                    FROM (
                        SELECT *, ROW_NUMBER() OVER (ORDER BY SupplierName) AS RowNum
                        FROM Suppliers
	                    WHERE (@searchValue = N'') OR ((SupplierName LIKE @searchValue) OR (Address LIKE @searchValue) OR (ContactName LIKE @searchValue) OR (Phone LIKE @searchValue))
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
                    data.Add(new Supplier()
                    {
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        SupplierName = Convert.ToString(dbReader["SupplierName"]),
                        City = Convert.ToString(dbReader["City"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Country = Convert.ToString(dbReader["Country"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Address = Convert.ToString(dbReader["Address"]),
                        Phone = Convert.ToString(dbReader["Phone"])
                    });
                }

                cn.Close();
            }

            return data;
        }
        /// <summary>
        /// Chỉnh sửa nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Supplier data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"UPDATE Suppliers SET Phone = @Phone, SupplierName = @SupplierName, ContactName = @contactName, Address = @address, City = @city, PostalCode = @postalCode, Country = @country
                    WHERE SupplierID = @SupplierID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
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
