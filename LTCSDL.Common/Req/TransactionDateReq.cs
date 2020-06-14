using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class TransactionDateReq
    {
        public int page { get; set; }

        public int size { get; set; }
        public DateTime date { get; set; }
    }
}
