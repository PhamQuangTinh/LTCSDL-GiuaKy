using LTCSDL.Common.BLL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL;
using LTCSDL.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

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

        public SingleRsp CreateNewProduct(CreateAndEditProduct req) 
        {
            Product pro = new Product();
            pro.Id = req.Id;
            pro.CatelogId = req.CatelogId;
            pro.Description = req.Description;
            pro.Price = req.Price;
            pro.Productcontent = req.Productcontent;
            pro.ProductImgLink = req.ProductImgLink;
            pro.Productname = req.Productname;
            pro.ProductInventory = req.ProductInventory;
            return _rep.CreateNewProduct(pro);
        }


        public SingleRsp UpdateProduct(CreateAndEditProduct req)
        {
            Product pro = new Product();
            pro.Id = req.Id;
            pro.CatelogId = req.CatelogId;
            pro.Description = req.Description;
            pro.Price = req.Price;
            pro.Productcontent = req.Productcontent;
            pro.ProductImgLink = req.ProductImgLink;
            pro.Productname = req.Productname;
            pro.ProductInventory = req.ProductInventory;
            return _rep.UpdateProduct(pro);
        }

        public SingleRsp RemoveProductByModel(CreateAndEditProduct req)
        {
            Product pro = new Product();
            pro.Id = req.Id;
            pro.CatelogId = req.CatelogId;
            pro.Description = req.Description;
            pro.Price = req.Price;
            pro.Productcontent = req.Productcontent;
            pro.ProductImgLink = req.ProductImgLink;
            pro.Productname = req.Productname;
            pro.ProductInventory = req.ProductInventory;
            return _rep.RemoveProduct(pro);
        }

        public SingleRsp RemoveProductById(int id) {
            var res = new SingleRsp();
            var m = _rep.RemoveProductById(id);
            res.Data = m;
            return res;
        }




        public object SearchProduct(string keyword, int page, int size) {
            var pro = All.Where(x => x.Productname.Contains(keyword) || x.Description.Contains(keyword));
            var offset = (page - 1) * size;
            var total = pro.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
            var data = pro.OrderBy(x => x.Productname).Skip(offset).Take(size).ToList();

            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalPage,
                Page = page,
                Size = size,
            };

            return res;
        }

        public object searchProductsByCategory(string keyword, int page, int size, int categoryId)
        {
            return _rep.searchProductsByCategory(keyword, page, size, categoryId);
            
        }

        public object findProductsByCatelog(int catelogId) {
            return _rep.findProductsByCatelog(catelogId);
        }

        public object findProductBetweenPrice(decimal fPrice, decimal lPrice, int page, int size)
        {
            return _rep.findProductBetweenPrice(fPrice, lPrice, page,size);
        }

        public SingleRsp top3Product()
        {
            var res = new SingleRsp();
            var m = _rep.top3Product();
            res.Data = m;
            return res;
        }


    }
}
