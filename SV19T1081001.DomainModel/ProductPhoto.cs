﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DomainModel
{
    public class ProductPhoto
    {
        public int PhotoID { get; set; }
        public int  ProductID { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsHidden { get; set; }
    }
}
