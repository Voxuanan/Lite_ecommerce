using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV19T1081001.DataLayer;
using SV19T1081001.DomainModel;
using System.Configuration;

namespace SV19T1081001.BusinessLayer
{
    /// <summary>
    /// Cung cấp các chức năng xử lý dữ liệu chung
    /// </summary>
    public static class CommonDataService
    {
        private static readonly ICommonDAL<Category> categoryDB;
        private static readonly ICommonDAL<Customer> customerDB;
        private static readonly ICommonDAL<Supplier> supplierDB;
        private static readonly ICommonDAL<Shipper> shipperDB;
        private static readonly ICommonDAL<Employee> employeeDB;
        private static readonly ICommonDAL<Country> countryDB;
        private static readonly IProductDAL<Product> productDB;
        private static readonly IProductInfoDAL<ProductAttribute> productAttributeDB;
        private static readonly IProductInfoDAL<ProductPhoto> productPhotoDB;
        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            switch (provider)
            {
                case "SQLServer":
                    categoryDB = new DataLayer.SQLServer.CategoryDAL(connectionString);
                    customerDB = new DataLayer.SQLServer.CustomerDAL(connectionString);
                    supplierDB = new DataLayer.SQLServer.SupplierDAL(connectionString);
                    shipperDB = new DataLayer.SQLServer.ShipperDAL(connectionString);
                    employeeDB = new DataLayer.SQLServer.EmployeeDAL(connectionString);
                    countryDB = new DataLayer.SQLServer.CountryDAL(connectionString);
                    productDB = new DataLayer.SQLServer.ProductDAL(connectionString);
                    productAttributeDB = new DataLayer.SQLServer.ProductAttributesDAL(connectionString);
                    productPhotoDB = new DataLayer.SQLServer.ProductPhotoDAL(connectionString);
                    break;
                default:
                    //categoryDB = new DataLayer.FakeDB.CategoryDAL();
                    break;
            }
        }
        /// <summary>
        /// Lấy danh sách tất cả thuộc tính mặt hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductAttribute> ListOfAllProductAttribute(int productID)
        {
            return productAttributeDB.List(productID).ToList();
        }
        /// <summary>
        /// Lấy danh sách tất cả ảnh mặt hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductPhoto> ListOfAllProductPhoto(int productID)
        {
            return productPhotoDB.List(productID).ToList();
        }
        /// <summary>
        /// Lấy danh sách tất cả mặt hàng
        /// </summary>
        /// <returns></returns>
        public static List<Product> ListOfAllProduct()
        {
            return productDB.List().ToList();
        }
        /// <summary>
        /// Lấy danh sách tất cả quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountry()
        {
            return countryDB.List().ToList();
        }
        /// <summary>
        /// Lấy danh sách tất cả nhân viên
        /// </summary>
        /// <returns></returns>
        public static List<Employee> ListOfAllEmployee()
        {
            return employeeDB.List().ToList();
        }
        /// <summary>
        /// Lấy danh sách tất cả loại hàng
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfAllCategory()
        {
            return categoryDB.List().ToList();
        }
        /// <summary>
        /// Lây danh sách tất cả khách hàng
        /// </summary>
        /// <returns></returns>
        public static List<Customer> ListOfAllCustomer()
        {
            return customerDB.List().ToList();
        }
        /// <summary>
        /// Lấy danh sách tất cả nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> ListOfAllSupplier()
        {
            return supplierDB.List().ToList();
        }
        /// <summary>
        /// Lây danh sách tất cả người giao hàng
        /// </summary>
        /// <returns></returns>
        public static List<Shipper> ListOfAllShipper()
        {
            return shipperDB.List().ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách mặt hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="supplierID"></param>
        /// <param name="categoryID"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Product> ListOfProduct(int page, int pageSize, string searchValue,int supplierID,int categoryID, out int rowCount)
        {
            rowCount = productDB.Count(searchValue, supplierID, categoryID);
            return productDB.List(page, pageSize, searchValue,supplierID,categoryID).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployee(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách loại hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfCategory(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomer(int page, int pageSize, string searchValue, out int rowCount)
        {
                rowCount = customerDB.Count(searchValue);
                return customerDB.List(page,pageSize,searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSupplier(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách người giao hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShipper(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Thêm thuộc tính mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProductAttribute(ProductAttribute data)
        {
            return productAttributeDB.Add(data);
        }
        /// <summary>
        /// Thêm ảnh mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProductPhoto(ProductPhoto data)
        {
            return productPhotoDB.Add(data);
        }
        /// <summary>
        /// Thêm mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProduct(Product data)
        {
            return productDB.Add(data);
        }
        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// Thêm loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }
        /// <summary>
        /// Thêm người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }
        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }
        /// <summary>
        /// Thêm nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }
        /// <summary>
        /// Chỉnh sửa thuộc tính mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProductAttribute(ProductAttribute data)
        {
            return productAttributeDB.Update(data);
        }
        /// <summary>
        /// Chỉnh sửa ảnh mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProductPhoto(ProductPhoto data)
        {
            return productPhotoDB.Update(data);
        }
        /// <summary>
        /// Chỉnh sửa mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProduct(Product data)
        {
            return productDB.Update(data);
        }
        /// <summary>
        /// Chỉnh sửa khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// Chỉnh sửa loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }
        /// <summary>
        /// Chỉnh sửa nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }
        /// <summary>
        /// Chỉnh sửa người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }
        /// <summary>
        /// Chỉnh sửa nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }
        /// <summary>
        /// Xóa thuộc tính mặt hàng
        /// </summary>
        /// <param name="productAttributeID"></param>
        /// <returns></returns>
        public static bool DeleteProductAttribute(int productAttributeID)
        {
            return productAttributeDB.Delete(productAttributeID);
        }
        /// <summary>
        /// Xóa ảnh mặt hàng
        /// </summary>
        /// <param name="productPhotoID"></param>
        /// <returns></returns>
        public static bool DeleteProductPhoto(int productPhotoID)
        {
            return productPhotoDB.Delete(productPhotoID);
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool DeleteProduct(int productID)
        {
            if (productDB.InUsed(productID))
                return false;
            return productDB.Delete(productID);
        }
        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            if (customerDB.InUsed(customerID))
                return false;
            return customerDB.Delete(customerID);
        }
        /// <summary>
        /// Xóa loại hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            if (categoryDB.InUsed(categoryID))
                return false;
            return categoryDB.Delete(categoryID);
        }
        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            if (employeeDB.InUsed(employeeID))
                return false;
            return employeeDB.Delete(employeeID);
        }
        /// <summary>
        /// Xóa người giao hàng
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperID)
        {
            if (shipperDB.InUsed(shipperID))
                return false;
            return shipperDB.Delete(shipperID);
        }
        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            if (supplierDB.InUsed(supplierID))
                return false;
            return supplierDB.Delete(supplierID);
        }
        /// <summary>
        /// Lây thông tin thuộc tính mặt hàng
        /// </summary>
        /// <param name="productAttributeID"></param>
        /// <returns></returns>
        public static ProductAttribute GetProductAttribute(int productAttributeID)
        {
            return productAttributeDB.Get(productAttributeID);
        }
        /// <summary>
        /// Lấy thông tin ảnh mặt hàng
        /// </summary>
        /// <param name="productPhotoID"></param>
        /// <returns></returns>
        public static ProductPhoto GetProductPhoto(int productPhotoID)
        {
            return productPhotoDB.Get(productPhotoID);
        }
        /// <summary>
        /// Lấy thông tin mặt hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static Product GetProduct(int productID)
        {
            return productDB.Get(productID);
        }
        /// <summary>
        /// Lấy thông tin khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }
        /// <summary>
        /// Lấy thông tin loại hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }
        /// <summary>
        /// Lây thông tin nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }
        /// <summary>
        /// Lấy thông tin người giao hàng
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }
        /// <summary>
        /// Lấy thông tin nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }
        /// <summary>
        /// Kiểm tra mặt hàng có được sử dụng hay chưa
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool InUsedProduct(int productID)
        {
            return productDB.InUsed(productID);
        }
        /// <summary>
        /// Kiểm tra khách hàng có được sử dụng hay chưa
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }
        /// <summary>
        /// Kiểm tra loại hàng có được sử dụng hay chưa
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool InUsedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }
        /// <summary>
        /// Kiểm tra nhân viên có dược sử dụng hay chưa
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }
        /// <summary>
        /// Kiểm tra người giao hàng có được sử dụng hay chưa
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }
        /// <summary>
        /// Kiểm tra nhà cung cấp có được sử dụng hay chưa
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
    }
}
