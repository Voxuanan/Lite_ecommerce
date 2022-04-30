using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DataLayer
{
    public interface IProductInfoDAL<T> where T : class
    {
        IList<T> List(int productID);
        //int Count(int productID, int id);
        T Get(int id);
        int Add(T data);
        bool Update(T data);
        bool Delete(int id);
        //bool InUsed(int id);

    }
}
