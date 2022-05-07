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
    public class ProductPhotoDAL : _BaseDAL, IProductInfoDAL<ProductPhoto>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public ProductPhotoDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Thêm ảnh cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(ProductPhoto data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"INSERT INTO ProductPhotos
                    (ProductID, Photo, Description, DisplayOrder, IsHidden)
                    VALUES (@ProductID,@Photo,@Description,@DisplayOrder,@IsHidden)
                    SELECT SCOPE_IDENTITY()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Xóa ảnh mặt hàng
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public bool Delete(int photoID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"DELETE FROM ProductPhotos WHERE PhotoID = @PhotoID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@PhotoID", photoID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy thông tin ảnh mặt hàng
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public ProductPhoto Get(int photoID)
        {
            ProductPhoto result = new ProductPhoto();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"SELECT * FROM ProductPhotos WHERE PhotoID = @PhotoID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@PhotoID", photoID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new ProductPhoto()
                    {
                        PhotoID = Convert.ToInt32(dbReader["PhotoID"]),
                        Description = Convert.ToString(dbReader["Description"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        IsHidden = Convert.ToBoolean(dbReader["IsHidden"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                    };
                }

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy danh sách các ảnh mặt hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public IList<ProductPhoto> List(int productID)
        {
            List<ProductPhoto> data = new List<ProductPhoto>();
            using (SqlConnection cn = OpenConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"SELECT * FROM ProductPhotos
                    WHERE ProductID = @productID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);

                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new ProductPhoto()
                    {
                        PhotoID = Convert.ToInt32(dbReader["PhotoID"]),
                        Description = Convert.ToString(dbReader["Description"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        IsHidden = Convert.ToBoolean(dbReader["IsHidden"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                    });
                }

                cn.Close();
            }

            return data;
        }
        /// <summary>
        /// Chỉnh sửa ảnh mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(ProductPhoto data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"UPDATE ProductPhotos
                    SET ProductID =@ProductID, Photo =@Photo, Description =@Description, DisplayOrder =@DisplayOrder, IsHidden = @IsHidden
                    WHERE PhotoID = @PhotoID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                cmd.Parameters.AddWithValue("@PhotoID", data.PhotoID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
