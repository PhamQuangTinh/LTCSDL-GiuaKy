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

        public SingleRsp DeleteTransaction(int userId, int tranId) 
        {
            var res = new SingleRsp();
            var m = _rep.DeleteTransaction(userId, tranId);
            res.Data = m;
            return res;
        }



        public SingleRsp removeOrderProductsTransction(int tranId, List<ProIDvsProNumReq> array) {
            var res = new SingleRsp();
            var m = _rep.removeOrderProductsTransction(tranId,array);
            res.Data = m;
            return res;
        }

        public SingleRsp findTransactionByUserIdvsTranId(int userId, int tranID)
        {
            var res = new SingleRsp();
            var m = _rep.findTransactionByUserIdvsTranId(userId, tranID);
            res.Data = m;
            return res;
        }

        public SingleRsp findTransactionByUser(int userId) 
        {
            var res = new SingleRsp();
            var m = _rep.findTransactionByUser(userId);
            res.Data = m;
            return res;
        }

    }
}
