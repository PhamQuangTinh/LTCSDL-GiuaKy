using LTCSDL.Common.DAL;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.DAL
{
    public class BasketRep
    {

        private Common common = new Common();
        /*public SingleRsp AddNewProductToBasket(Basket bas)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (common.checkExistbyID(bas.Userid))
                        {
                            var t = context.Basket.Add(bas);
                            context.SaveChanges();
                            tran.Commit();
                        }
                        else
                        {
                            res.SetMessage("Don't Exist User");

                            tran.Rollback();
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

        public SingleRsp DeleteProductInBasket(Basket bas)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (!common.checkExistbyID(bas.Userid))
                        {
                            var t = context.Basket.Remove(bas);
                            context.SaveChanges();
                            tran.Commit();
                        }
                        else
                        {

                            res.SetMessage("No User Match");
                            tran.Rollback();
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

*/

    }
}
