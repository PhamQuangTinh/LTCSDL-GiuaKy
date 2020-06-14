using LTCSDL.Common.DAL;
using LTCSDL.Common.Rsp;
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

        public SingleRsp CreateNewCatelog(Catelog cate)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                            var t = context.Catelog.Add(cate);
                            res.Data = cate;
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

        public SingleRsp UpdateCatelog(Catelog cate)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Catelog.Update(cate);
                        res.Data = cate;
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

        public SingleRsp RemoveCatelog(Catelog cate)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Catelog.Remove(cate);
                        res.Data = cate;
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

        public object findCatelogPagination(int page, int size, string keyword)
        {
            var pro = All.Where(x => x.Name.Contains(keyword));


            var offset = (page - 1) * size;
            var total = pro.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
            var data = pro.OrderBy(x => x.Id).Skip(offset).Take(size).ToList();

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
    }
}
