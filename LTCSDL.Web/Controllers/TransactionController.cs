using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCSDL.BLL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LTCSDL.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public TransactionController()
        {
            _svc = new TransactionSvc();
        }


        [HttpPost("create-new-transaction")]
        public IActionResult CreateNewTransaction([FromBody] NewTransactionReq req)
        {
            var res = new SingleRsp();
            res = _svc.CreateNewTransaction(req.UserId, req.ProductId, req.Amount);
            return Ok(res);
        }

        [HttpPost("create-new-transaction-with-many-products")]
        public IActionResult CreateNewTransactionWithManyProducts([FromBody] TransactionManyProductsReq req)
        {
            var res = new SingleRsp();
            res = _svc.CreateNewTransactionWithManyProducts(req.UserId, req.Proreq, req.Amount);
            return Ok(res);
        }

        [HttpPost("find-transaction-by-transaction-id")]
        public IActionResult FindTransactionByTransactionId([FromBody] TransactionIDReq req)
        {
            var res = new SingleRsp();
            res = _svc.findTransactionByTransactionId(req.TransactionId);
            return Ok(res);
        }

        [HttpPost("remove-order-products-transaction")]
        public IActionResult RemoveOrderProductsTransction([FromBody] RemoveOrderProductsReq req)
        {
            var res = new SingleRsp();
            res = _svc.removeOrderProductsTransction(req.TransactionId, req.Proreq);
            return Ok(res);
        }


        [HttpPost("remove-transaction")]
        public IActionResult DeleteTransaction([FromBody] TranIdvsUserIdReq req)
        {
            var res = new SingleRsp();
            res = _svc.DeleteTransaction(req.userId, req.tranId);
            return Ok(res);
        }

        [HttpPost("find-transaction-by-user-and-tran")]
        public IActionResult FindTransactionByUserIdvsTranId([FromBody] TranIdvsUserIdReq req)
        {
            var res = new SingleRsp();
            res = _svc.findTransactionByUserIdvsTranId(req.userId, req.tranId);
            return Ok(res);
        }


        [HttpPost("find-transaction-by-user")]
        public IActionResult FindTransactionByUser([FromBody] UserIdReq req)
        {
            var res = new SingleRsp();
            res = _svc.findTransactionByUser(req.UserId);
            return Ok(res);
        }



        [HttpPost("search-transaction-pagination")]

        public IActionResult FindTransactionPagination([FromBody] UserPaginationReq req)
        {

            var res = new SingleRsp();
            object m = null;
            if (req.id == 0)
            {
                m = _svc.findTransactionPagination(req.page, req.size);

            }
            else if (req.id == 1)
            {
                m = _svc.findByDateTransaction(req.page, req.size, req.dateTime1, req.dateTime2);
            }
            res.Data = m;
            return Ok(res);
        }


        [HttpPost("Statistical-by-date")]
        public IActionResult StatisticalByDate([FromBody] UserPaginationReq req)
        {
            var res = new SingleRsp();
            var m = _svc.StatisticalByDate(req.page, req.size, req.dateTime1, req.dateTime2);
            res.Data = m;
            return Ok(res);
        }



        public TransactionSvc _svc;
    }
}