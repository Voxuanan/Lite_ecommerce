﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DataLayer
{
    public interface IProductDAL<Product>
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