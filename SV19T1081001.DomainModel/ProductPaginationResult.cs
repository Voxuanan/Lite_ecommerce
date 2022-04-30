using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DomainModel
{
    public class ProductPaginationResult : BasePaginationResult
    {
        public List<Product> Data { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
    }
}
