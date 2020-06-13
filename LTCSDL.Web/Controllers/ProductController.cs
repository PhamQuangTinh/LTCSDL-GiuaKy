using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LTCSDL.BLL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LTCSDL.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        public ProductController()
        {
            _svc = new ProductSvc();
        }
        
        [HttpPost("find-product-by-id")]
        public IActionResult FindProductById([FromBody] FindProductByIdReq req)
        {
            var res = new SingleRsp();
            res = _svc.Read(req.ProductId);
            return Ok(res);
        }


        [HttpPost("find-all-product")]
        public IActionResult FindAll()
        {
            var res = new SingleRsp();
            res = _svc.findAll();
            return Ok(res);
        }

        [HttpPost("create-new-product")]
        public IActionResult CreateNewProduct([FromBody] CreateAndEditProduct req)
        {
            var res = new SingleRsp();
            res = _svc.CreateNewProduct(req);
            return Ok(res);
        }

        [HttpPost("edit-product")]
        public IActionResult EditProduct([FromBody] CreateAndEditProduct req)
        {
            var res = new SingleRsp();
            res = _svc.UpdateProduct(req);
            return Ok(res);
        }

        [HttpPost("remove-product-by-model")]
        public IActionResult RemoveProduct([FromBody] CreateAndEditProduct req)
        {
            var res = new SingleRsp();
            res = _svc.RemoveProductByModel(req);
            return Ok(res);
        }

        [HttpPost("remove-product-by-id")]
        public IActionResult RemoveProductById([FromBody] ProductIDReq req)
        {
            var res = new SingleRsp();
            res = _svc.RemoveProductById(req.ProductId);
            return Ok(res);
        }


        [HttpPost("search-product")]
        public IActionResult SearchProduct([FromBody]SearchProductReq req)
        {
            var res = new SingleRsp();
            var m = new object();
            if(req.Id == 0)
            {
                if (req.categoryId == 0)
                {
                    m = _svc.SearchProduct(req.keyword, req.Page, req.Size);
                }
                else
                {
                    m = _svc.searchProductsByCategory(req.keyword, req.Page, req.Size, req.categoryId);
                }
            }
            else if(req.Id == 1)
            {
                m = _svc.findProductBetweenPrice(req.FPrice, req.LPrice, req.Page, req.Size);
            }
            res.Data = m;

            return Ok(res);
        }

        

        [HttpPost("find-product-by-category")]
        public IActionResult FindProductByCategory([FromBody]CategoryIdReq req)
        {
            var res = new SingleRsp();
            var m = _svc.findProductsByCatelog(req.CatelogId);
            res.Data = m;
            return Ok(res);
        }

        [HttpPost("find-product-between-price")]
        public IActionResult FindProductBetweenPrice([FromBody]PricesProductReq req)
        {
            var res = new SingleRsp();
            var m = _svc.findProductBetweenPrice(req.FPrice,req.LPrice,req.page,req.size);
            res.Data = m;
            return Ok(res);
        }

        [HttpPost("top-product")]
        public IActionResult Top3Product()
        {
            var res = new SingleRsp();
            res = _svc.top3Product();
            
            return Ok(res);
        }

        private readonly ProductSvc _svc;

    }
}