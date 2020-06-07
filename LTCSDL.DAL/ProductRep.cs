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

        public List<object> findProductBetweenPrice(decimal fPrice, decimal lPrice)
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
                cmd.CommandText = "timkiemsanphamtheokhoangia";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@kg1", fPrice);
                cmd.Parameters.AddWithValue("@kg2", lPrice);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            Id = row["id"],
                            CatelogId = row["catelog_id"],
                            Productname = row["productname"],
                            Price = row["price"],
                            Description = row["description"],
                            Productcontent = row["productcontent"],
                            ProductInventory = row["product_inventory"],
                            ProductImgLink = row["product_img_link"],
                            Discount = row["discount"],
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



        private Boolean checkCategory(int id)
        {
            var cate = Context.Catelog.FirstOrDefault(x => x.Id == id);
            if (cate != null)
                return true;
            return false;
        }
    }
}
