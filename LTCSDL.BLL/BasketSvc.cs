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
    public class BasketSvc : GenericSvc<BasketRep, Basket>
    {
        public SingleRsp AddNewProductToBasket(NewBasketReq req)
        {
            Basket bas = new Basket();
            bas.Proid = req.Proid;
            bas.Userid = req.Userid;
            bas.Productname = req.Productname;
            bas.Price = req.Price;
            bas.ProductInventory = req.ProductInventory;
            bas.ProductImgLink = req.ProductImgLink;
            var res = new SingleRsp();
            var m = _rep.AddNewProductToBasket(req.Proid, req.Userid, req.Productname, req.Price, req.ProductInventory, req.ProductImgLink);
            res.Data = m;
            return res;
        }


        public SingleRsp DeleteBasket(int UserId)
        {

            var res = new SingleRsp();
            var m = _rep.DeleteBasket(UserId);
            res.Data = m;
            return res;
        }

        public SingleRsp DeleteProductsInBasket(int UserId, int ProId)
        {

            var res = new SingleRsp();
            var m = _rep.DeleteProductsInBasket(UserId, ProId);
            res.Data = m;
            return res;
        }

        public SingleRsp GetBasket(int UserId)
        {
            var res = new SingleRsp();
            var m = _rep.GetBasket(UserId);
            res.Data = m;
            return res;
        }

        public SingleRsp UpdateProdcutBasket(int userId, int proId, int proInvent) {
            var res = new SingleRsp();
            var m = _rep.UpdateProdcutBasket(userId,proId,proInvent);
            res.Data = m;
            return res;
        }
    }
}
