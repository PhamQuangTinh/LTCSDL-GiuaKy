using LTCSDL.Common.DAL;
using LTCSDL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LTCSDL.DAL
{
    public class CatelogRep : GenericRep<MyPhamContext, Catelog>
    {
        public List<Catelog> getAllCategoryName()
        {
            var res = All.Select(x => x).ToList();
            return res;
        }

        public object getCategoryProduct(int categoryId)
        {
            var res = All.Where(x => x.Id == categoryId);

            var data = res.Join(Context.Product, a => a.Id, b => b.CatelogId, (a, b) => new
            {
                b.Id,
                b.CatelogId,
                b.Productname,
                b.Price,
                b.Description,
                b.Productcontent,
                b.ProductInventory,
                b.ProductImgLink
            }).ToList();

            return data;
        }
    }
}
