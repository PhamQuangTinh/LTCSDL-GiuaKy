using LTCSDL.Common.BLL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL;
using LTCSDL.DAL.Models;

namespace LTCSDL.BLL
{
    public class ProductSvc  : GenericSvc<ProductRep, Product>
    {
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        public SingleRsp findAll() {
            var res = new SingleRsp();

            var m = _rep.findAll();
            res.Data = m;
            return res;

        }
    }
}
