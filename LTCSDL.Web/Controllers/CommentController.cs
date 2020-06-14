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
    public class CommentController : ControllerBase
    {
        public CommentController()
        {
            _svc = new CommentSvc();
        }
        [HttpPost("get-comment-by-id")]
        public IActionResult getCommentID([FromBody] SimpleReq req)
        {
            var res = new SingleRsp();
            res = _svc.Read(req.Id);
            return Ok(res);
        }

        [HttpPost("create-comment")]
        public IActionResult RemoveProductByModel([FromBody] CreateCommentReq req)
        {
            var res = _svc.CreateComment(req.UserId,req.ProductId,req.CommentContent);
            return Ok(res);
        }

        [HttpPost("update-comment")]
        public IActionResult UpdateCommon([FromBody] CreateCommentReq req)
        {
            var res = _svc.UpdateComment(req);
            return Ok(res);
        }


        [HttpPost("get-comment-product")]
        public IActionResult GetAllCommentProduct([FromBody] ProductIDReq req)
        {
            var res = _svc.GetAllCommentProduct(req.ProductId);
            return Ok(res);
        }

        [HttpPost("remove-comment")]
        public IActionResult RemoveComment([FromBody] CommentReq req)
        {
            var res = new SingleRsp();
            res = _svc.RemoveComment(req);
            return Ok(res);
        }


        private readonly CommentSvc _svc;
    }
}