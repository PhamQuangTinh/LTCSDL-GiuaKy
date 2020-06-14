using LTCSDL.Common.BLL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL;
using LTCSDL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.BLL
{
    public class CatelogSvc : GenericSvc<CatelogRep, Catelog>
    {
        public SingleRsp getAllCategoryName()
        {
            var res = new SingleRsp();
            var m = _rep.getAllCategoryName();
            res.Data = m;
            return res;
        }

        public SingleRsp getCategoryProduct(int categoryId)
        {
            var res = new SingleRsp();
            var m = _rep.getCategoryProduct(categoryId);
            res.Data = m;
            return res;
        }

        public SingleRsp CreateNewCatelog(CatelogReq req)
        {
            Catelog cate = new Catelog();
            cate.Id = req.Id;
            cate.Name = req.Name;
            return _rep.CreateNewCatelog(cate);
        }
        public SingleRsp UpdateCatelog(CatelogReq req)
        {
            Catelog cate = new Catelog();
            cate.Id = req.Id;
            cate.Name = req.Name;
            return _rep.UpdateCatelog(cate);
        }
        public SingleRsp RemoveCatelog(CatelogReq req)
        {
            Catelog cate = new Catelog();
            cate.Id = req.Id;
            cate.Name = req.Name;
            return _rep.RemoveCatelog(cate);
        }

        public object findCatelogPagination(int page, int size, string keyword)
        {
            return _rep.findCatelogPagination(page, size, keyword);
        }
    }
}
