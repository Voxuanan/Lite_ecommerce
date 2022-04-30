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
        public static List<ProductAttribute> ListOfAllProductAttribute(int productID)
        {
            return productAttributeDB.List(productID).ToList();
        }
        public static List<ProductPhoto> ListOfAllProductPhoto(int productID)
        {
            return productPhotoDB.List(productID).ToList();
        }
        public static List<Product> ListOfAllProduct()
        {
            return productDB.List().ToList();
        }
        public static List<Country> ListOfCountry()
        {
            return countryDB.List().ToList();
        }
        public static List<Employee> ListOfAllEmployee()
        {
            return employeeDB.List().ToList();
        }
 
        public static List<Category> ListOfAllCategory()
        {
            return categoryDB.List().ToList();
        }
        public static List<Customer> ListOfAllCustomer()
        {
            return customerDB.List().ToList();
        }
        public static List<Supplier> ListOfAllSupplier()
        {
            return supplierDB.List().ToList();
        }
        public static List<Shipper> ListOfAllShipper()
        {
            return shipperDB.List().ToList();
        }

        public static List<Product> ListOfProduct(int page, int pageSize, string searchValue,int supplierID,int categoryID, out int rowCount)
        {
            rowCount = productDB.Count(searchValue, supplierID, categoryID);
            return productDB.List(page, pageSize, searchValue,supplierID,categoryID).ToList();
        }
        public static List<Employee> ListOfEmployee(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Lấy danh sách các mặt hàng
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

        public static List<Supplier> ListOfSupplier(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }

        public static List<Shipper> ListOfShipper(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        public static int AddProductAttribute(ProductAttribute data)
        {
            return productAttributeDB.Add(data);
        }
        public static int AddProductPhoto(ProductPhoto data)
        {
            return productPhotoDB.Add(data);
        }
        public static int AddProduct(Product data)
        {
            return productDB.Add(data);
        }
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }
        public static bool UpdateProductAttribute(ProductAttribute data)
        {
            return productAttributeDB.Update(data);
        }
        public static bool UpdateProductPhoto(ProductPhoto data)
        {
            return productPhotoDB.Update(data);
        }
        public static bool UpdateProduct(Product data)
        {
            return productDB.Update(data);
        }
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }
        public static bool DeleteProductAttribute(int productAttributeID)
        {
            return productAttributeDB.Delete(productAttributeID);
        }
        public static bool DeleteProductPhoto(int productPhotoID)
        {
            return productPhotoDB.Delete(productPhotoID);
        }
        public static bool DeleteProduct(int productID)
        {
            if (productDB.InUsed(productID))
                return false;
            return productDB.Delete(productID);
        }
        public static bool DeleteCustomer(int customerID)
        {
            if (customerDB.InUsed(customerID))
                return false;
            return customerDB.Delete(customerID);
        }
        public static bool DeleteCategory(int categoryID)
        {
            if (categoryDB.InUsed(categoryID))
                return false;
            return categoryDB.Delete(categoryID);
        }
        public static bool DeleteEmployee(int employeeID)
        {
            if (employeeDB.InUsed(employeeID))
                return false;
            return employeeDB.Delete(employeeID);
        }
        public static bool DeleteShipper(int shipperID)
        {
            if (shipperDB.InUsed(shipperID))
                return false;
            return shipperDB.Delete(shipperID);
        }
        public static bool DeleteSupplier(int supplierID)
        {
            if (supplierDB.InUsed(supplierID))
                return false;
            return supplierDB.Delete(supplierID);
        }
        public static ProductAttribute GetProductAttribute(int productAttributeID)
        {
            return productAttributeDB.Get(productAttributeID);
        }
        public static ProductPhoto GetProductPhoto(int productPhotoID)
        {
            return productPhotoDB.Get(productPhotoID);
        }
        public static Product GetProduct(int productID)
        {
            return productDB.Get(productID);
        }
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }
        public static bool InUsedProduct(int productID)
        {
            return productDB.InUsed(productID);
        }
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }
        public static bool InUsedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }
        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
    }
}
