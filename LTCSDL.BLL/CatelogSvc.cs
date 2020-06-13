using LTCSDL.Common.BLL;
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
    }
}
