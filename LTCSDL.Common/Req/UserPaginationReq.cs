using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class UserPaginationReq
    {
        public int page { get; set; }
        public int size { get; set; }
        public string keyword { get; set; }
        public int id { get; set; }

        public DateTime dateTime1 { get; set; }

        public DateTime dateTime2 { get; set; }
    }
}
