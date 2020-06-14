using LTCSDL.Common.BLL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL;
using LTCSDL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LTCSDL.BLL
{
    public class CommentSvc : GenericSvc<CommentRep, Comment>
    {
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        public SingleRsp CreateComment(int userId, int proId, String content)
        {
            var res = new SingleRsp();
            var m = _rep.CreateComment(userId,proId,content);
            res.Data = m;
            return res;
            
        }

        public SingleRsp UpdateComment(CreateCommentReq req)
        {
            var res = new SingleRsp();
            Comment cmt = new Comment();
            cmt.Id = req.Id;
            cmt.UserId = req.UserId;
            cmt.ProductId = req.ProductId;
            cmt.CommentContent = req.CommentContent;
            var m = _rep.UpdateCommon(cmt);
            res.Data = m;
            return res;
        }

        public SingleRsp GetAllCommentProduct(int productId)
        {
            var res = new SingleRsp();
            var m = _rep.GetAllCommentProduct(productId);
            res.Data = m;
            return res;
        }

        public SingleRsp RemoveComment(CommentReq req)
        {
            Comment cmt = new Comment();
            cmt.Id = req.Id;
            cmt.UserId = req.UserId;
            cmt.UserName = req.UserName;
            cmt.ProductId = req.ProductId;
            cmt.CommentContent = req.CommentContent;
            cmt.TimeComment = req.TimeComment;
            return _rep.RemoveComment(cmt);

        }


    }
}
