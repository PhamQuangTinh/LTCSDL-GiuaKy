using LTCSDL.Common.DAL;
using LTCSDL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LTCSDL.DAL
{
    public class ProductRep : GenericRep<MyPhamContext, Product>
    {
        public override Product Read(int id)
        {
            var res = All.FirstOrDefault(p => p.Id == id);
            return res;
        }

        public List<Product> findAll() {
            List<Product> res = null;
            res = All.Select(x => x).ToList();
            return res;
        }
    }
}
