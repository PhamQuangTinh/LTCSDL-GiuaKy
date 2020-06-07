using System;
using System.Collections.Generic;

namespace LTCSDL.DAL.Models
{
    public partial class Basket
    {
        public int Proid { get; set; }
        public int Userid { get; set; }
        public string Productname { get; set; }
        public decimal Price { get; set; }
        public int ProductInventory { get; set; }
        public string ProductImgLink { get; set; }
    }
}
