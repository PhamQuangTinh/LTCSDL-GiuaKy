using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class RemoveOrderProductsReq
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }

        public List<ProIDvsProNumReq> Proreq { get; set; }
    }
}
