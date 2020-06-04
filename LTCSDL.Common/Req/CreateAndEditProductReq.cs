using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class CreateAndEditProduct
    {
        public int Id { get; set; }
        public int CatelogId { get; set; }
        public string Productname { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Productcontent { get; set; }
        public int ProductInventory { get; set; }
        public string ProductImgLink { get; set; }
        public decimal? Discount { get; set; }

    }
}
