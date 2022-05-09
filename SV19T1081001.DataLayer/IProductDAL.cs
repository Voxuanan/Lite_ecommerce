using SV19T1081001.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DataLayer
{
    /// <summary>
    /// Đinh nghĩa các phép xử lý dữ liệu trên mặt hàng
    /// </summary>
    /// <typeparam name="Product"></typeparam>
    public interface IProductDAL
    {
        IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int supplierID = 0, int categoryID = 0);
        int Count(string searchValue, int supplierID = 0, int categoryID = 0);
        Product Get(int id);
        int Add(Product data);
        bool Update(Product data);
        bool Delete(int id);
        bool InUsed(int id);
    }
}
