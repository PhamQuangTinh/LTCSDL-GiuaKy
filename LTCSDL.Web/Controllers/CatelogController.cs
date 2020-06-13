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
    public class CatelogController : ControllerBase
    {
        public CatelogController()
        {
            _svc = new CatelogSvc();
        }
        [HttpPost("get-all-catelog")]
        public IActionResult getDangNhapyID()
        {
            var res = new SingleRsp();
            res = _svc.getAllCategoryName();
            return Ok(res);
        }

        [HttpPost("get-catelog-product")]
        public IActionResult getCategoryProduct([FromBody] CatelogIdReq req)
        {
            var res = new SingleRsp();
            res = _svc.getCategoryProduct(req.categoryId);
            return Ok(res);
        }

        private CatelogSvc _svc;
    }
}