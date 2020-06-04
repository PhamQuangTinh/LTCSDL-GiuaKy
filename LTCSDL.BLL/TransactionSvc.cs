using LTCSDL.Common.BLL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL;
using LTCSDL.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace LTCSDL.BLL
{
    public class TransactionSvc : GenericSvc<TransactionRep, Transaction>
    {
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        public SingleRsp CreateNewTransaction(int userId, int proId, decimal amount) 
        {
            var res = new SingleRsp();
            var m = _rep.CreateNewTransaction(userId, proId, amount);
            res.Data = m;
            return res;
        }

        public SingleRsp CreateNewTransactionWithManyProducts(int userId, List<ProIDvsProNumReq> a, decimal amount)
        {
            var res = new SingleRsp();
            var m = _rep.CreateNewTransactionWithManyProducts(userId, a, amount);
            res.Data = m;
            return res;
        }

        public SingleRsp findTransactionByTransactionId(int id)
        {
            var res = new SingleRsp();
            var m = _rep.findTransactionByTransactionId(id);
            res.Data = m;
            return res;
        }

    }
}
