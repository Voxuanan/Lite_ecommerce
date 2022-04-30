using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DomainModel
{
    public class CategoryPaginationResult : BasePaginationResult
    {
        public List<Category> Data { get; set; }
    }
}
