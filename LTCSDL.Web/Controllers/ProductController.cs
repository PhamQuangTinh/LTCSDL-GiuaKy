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
        [Authorize]
        public IActionResult FindProductById([FromBody] FindProductByIdReq req)
        {
            var res = new SingleRsp();
            res = _svc.Read(req.Id);
            return Ok(res);
        }


        [HttpPost("find-all-product")]
        public IActionResult FindAll()
        {
            var res = new SingleRsp();
            res = _svc.findAll();
            return Ok(res);
        }

        private readonly ProductSvc _svc;

    }
}