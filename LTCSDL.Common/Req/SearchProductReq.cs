using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class SearchProductReq
    {

        public int Page { get; set; }

        public int Size { get; set; }

        public int Id { get; set; }

        public int categoryId { get; set; }
        
        public string Type { get; set; }

        public decimal FPrice { get; set; }

        public decimal LPrice { get; set; }

        public  string keyword { get; set; }
    }
}
