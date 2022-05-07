using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DataLayer
{
    /// <summary>
    /// Định nghĩa các phép xử lí dữ liệu trên thuộc tính , ảnh mặt hàng
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProductInfoDAL<T> where T : class
    {
        IList<T> List(int productID);
        T Get(int id);
        int Add(T data);
        bool Update(T data);
        bool Delete(int id);
    }
}
