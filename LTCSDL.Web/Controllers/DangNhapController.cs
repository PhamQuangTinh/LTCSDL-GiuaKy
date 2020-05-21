using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCSDL.BLL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LTCSDL.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DangNhapController : ControllerBase
    {
        public DangNhapController() {
            _svc = new DangNhapSvc();
        }

        [HttpPost("get-by-id")]
        public IActionResult getDangNhapyID([FromBody] SimpleReq req)
        {
            var res = new SingleRsp();
            res = _svc.Read(req.Id);
            return Ok(res);
        }


        [HttpPost("get-by-userName")]
        public IActionResult getUserName([FromBody] LoginReq req)
        {
            var res = new SingleRsp();
            res = _svc.findByUserNameAndPassWord(req);
            return Ok(res);
        }



        [HttpPost("create-new-user")]
        public IActionResult CreateNewUser([FromBody]CreateNewUserAccountReq req)
        {
            var res = new SingleRsp();
            res = _svc.CreateNewUser(req);
            return Ok(res);
        }

        [HttpPost("update-user-imformation")]
        public IActionResult UpdateUser([FromBody]CreateNewUserAccountReq req)
        {
            var res = new SingleRsp();
            res = _svc.UpdateUser(req);
            return Ok(res);
        }

        [HttpPost("remove-user")]
        public IActionResult deleteUserName([FromBody] CreateNewUserAccountReq req)
        {
            var res = new SingleRsp();
            res = _svc.RemoveUser(req);
            return Ok(res);
        }



        private readonly DangNhapSvc _svc;
    }
}
