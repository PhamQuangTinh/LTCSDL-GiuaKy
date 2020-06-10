using LTCSDL.Common.Rsp;
using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class NewBasketReq
    {
        public int Proid { get; set; }
        public int Userid { get; set; }
        public string Productname { get; set; }
        public decimal Price { get; set; }
        public int ProductInventory { get; set; }
        public string ProductImgLink { get; set; }

    }
}
