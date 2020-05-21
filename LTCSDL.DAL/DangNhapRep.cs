using LTCSDL.Common.DAL;
using LTCSDL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.DAL
{
    using LTCSDL.Common.Rsp;
    using Models;
    using System.Linq;

    public class DangNhapRep : GenericRep<MyPhamContext, Dangnhap>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Dangnhap Read(int id)
        {
            var res = All.FirstOrDefault(p => p.Id == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.Id == id);
            m = base.Delete(m); //TODO
            return m.Id;
        }

        public Dangnhap findByUserNameAndPassWord(String Username, String password) {
            var res = All.FirstOrDefault(p => p.Username == Username && p.Password== password);

            return res;
        }

        public SingleRsp CreateNewUser(Dangnhap dn)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (checkExistbyUserName(dn.Username))
                        {
                            var t = context.Dangnhap.Add(dn);
                            context.SaveChanges();
                            tran.Commit();
                        }
                        else
                        {
                            res.SetMessage("Exist User");
                         
                            tran.Rollback();
                        }
                        

                    }catch(Exception e)
                    {
                        tran.Rollback();
                        res.SetError(e.StackTrace);
                    }
                }

            }

            return res;
        }

        public SingleRsp UpdateUser(Dangnhap dn)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (!checkExistbyID(dn.Id))
                        {
                            var t = context.Dangnhap.Update(dn);
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

        public SingleRsp DeleteUser(Dangnhap dn)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (!checkExistbyID(dn.Id))
                        {
                            var t = context.Dangnhap.Remove(dn);
                            context.SaveChanges();
                            tran.Commit();
                        }
                        else {

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




        public Boolean checkExistbyUserName(String username) {
            var id = All.FirstOrDefault(p => p.Username == username);
            if (id == null) {
                return true;
            }
            return false;
        }

        public Boolean checkExistbyID(int Id)
        {
            var id = All.FirstOrDefault(p => p.Id == Id);
            if (id == null)
            {
                return true;
            }
            return false;
        }


       
        #endregion


    }
}
