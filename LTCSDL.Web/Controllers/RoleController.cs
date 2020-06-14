using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCSDL.BLL;
using LTCSDL.Common.Rsp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTCSDL.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public RoleController()
        {
            _svc = new RoleSvc();
        }

        [HttpPost("get-all-role")]
        public IActionResult GetAllRole()
        {
            var res = new SingleRsp();
            res = _svc.getAllRole();
            return Ok(res);
        }

        private RoleSvc _svc;
    }
}