using LTCSDL.Common.DAL;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
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

        public List<Product> findAll()
        {
            List<Product> res = null;
            res = All.Select(x => x).ToList();
            return res;
        }

        public int RemoveProductById(int id)
        {
            var m = base.All.First(x => x.Id == id);
            m = base.Delete(m);
            return m.Id;
        }

        public SingleRsp CreateNewProduct(Product pro)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (checkCategory(pro.CatelogId)) {
                            var t = context.Product.Add(pro);
                            res.Data = pro;
                            context.SaveChanges();
                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                            res.SetMessage("khong tim thay category");
                        }
                        
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        res.SetError(e.StackTrace);
                    }
                }

            }

            return res;
        }

        public SingleRsp RemoveProduct(Product pro)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Product.Remove(pro);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        res.SetError(e.StackTrace);
                    }
                }

            }

            return res;
        }

        public SingleRsp UpdateProduct(Product pro)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (checkCategory(pro.CatelogId))
                        {
                            var t = context.Product.Update(pro);
                            res.Data = pro;
                            context.SaveChanges();
                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                            res.SetMessage("khong tim thay category");
                        }
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        res.SetError(e.StackTrace);
                    }
                }

            }

            return res;
        }


        public object findProductsByCatelog(int catelogId)
        {
            var data = Context.Product.Join(Context.Catelog, a => a.CatelogId, b => b.Id, (a, b) => new
            {
                a.Id,
                a.CatelogId,
                a.Productname,
                a.Price,
                a.Description,
                a.Productcontent,
                a.ProductInventory,
                a.ProductImgLink,
            }).Where(x => x.CatelogId == catelogId)
            .OrderBy(x => x.Productname)
            .ToList();

            return data;
        }

        public object findProductBetweenPrice(decimal fPrice, decimal lPrice, int page, int size)
        {
            var pro = All.Where(x => x.Price >= fPrice && x.Price <= lPrice);


            var offset = (page - 1) * size;
            var total = pro.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
            var data = pro.OrderBy(x => x.Price).Skip(offset).Take(size).ToList();

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

        public List<object> top3Product()
        {
            List<object> res = new List<object>();
            var cmn = (SqlConnection)Context.Database.GetDbConnection();
            if (cmn.State == ConnectionState.Closed)
            {
                cmn.Open();
            }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cmn.CreateCommand();
                cmd.CommandText = "Top3Product";
                cmd.CommandType = CommandType.StoredProcedure;

                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            Id = row["product_id"],
                            ProductImgLink = row["product_img_link"],
                            rank = row["rank"],
                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception)
            {
                res = null;
            }

            return res;
        }


        public object searchProductsByCategory(string keyword,int page, int size, int categoryId)
        {
            var pro = All.Where(x => x.Productname.Contains(keyword) && x.CatelogId == categoryId);
            
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



        private Boolean checkCategory(int id)
        {
            var cate = Context.Catelog.FirstOrDefault(x => x.Id == id);
            if (cate != null)
                return true;
            return false;
        }
    }
}
