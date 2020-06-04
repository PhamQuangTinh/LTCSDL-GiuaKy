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
            res = _svc.removeOrderProductsTransction(req.TransactionId, req.Proreq, req.Amount);
            return Ok(res);
        }


        [HttpPost("remove-transaction")]
        public IActionResult DeleteTransaction([FromBody] TransactionReq req)
        {
            var res = new SingleRsp();
            res = _svc.DeleteTransaction(req);
            return Ok(res);
        }

        public TransactionSvc _svc;
    }
}