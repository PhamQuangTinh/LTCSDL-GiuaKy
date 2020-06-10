using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCSDL.BLL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTCSDL.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        
        public BasketController()
        {
            _svc = new BasketSvc();
        }
        [HttpPost("create-product-basket")]
        public IActionResult CreateNewTransaction([FromBody] NewBasketReq req)
        {
            var res = new SingleRsp();
            res = _svc.AddNewProductToBasket(req);
            return Ok(res);
        }

        [HttpPost("delete-basket")]
        public IActionResult DeleteBasket([FromBody] UserIdReq req)
        {
            var res = new SingleRsp();
            res = _svc.DeleteBasket(req.UserId);
            return Ok(res);
        }
        [HttpPost("delete-product-basket")]
        public IActionResult DeleteProductsInBasket([FromBody] UserIDvsProIDReq req)
        {
            var res = new SingleRsp();
            res = _svc.DeleteProductsInBasket(req.UserId, req.ProId);
            return Ok(res);
        }

        [HttpPost("get-basket")]
        public IActionResult GetBasket([FromBody] UserIdReq req)
        {
            var res = new SingleRsp();
            res = _svc.GetBasket(req.UserId);
            return Ok(res);
        }

        [HttpPost("update-basket")]
        public IActionResult UpdateProdcutBasket([FromBody] UserIdProIdProIvent req)
        {
            var res = new SingleRsp();
            res = _svc.UpdateProdcutBasket(req.userId,req.proId,req.proIvent);
            return Ok(res);
        }
        private BasketSvc _svc;
    }
}