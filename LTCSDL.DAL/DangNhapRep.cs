using LTCSDL.Common.DAL;
using LTCSDL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.DAL
{
    using LTCSDL.Common.Rsp;
    using Microsoft.EntityFrameworkCore.Internal;
    using Models;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    public class DangNhapRep : GenericRep<MyPhamContext, User>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override User Read(int id)
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


        public User findUserByUsername(String username)
        {
            var res = All.FirstOrDefault(p => p.Username.Equals(username));
            return res;
        }

        public User findByUserNameAndPassWord(String username, String password)
        {
            /*var context = new MyPhamContext();
            var res = from s in Context.User
                      join r in Context.Role on s.Roleid equals r.Id
                      where s.Username == Username && s.Password == password
                      select s;
            
            return res.FirstOrDefault();*/
            var res = Context.User.Join(Context.Role, a => a.Roleid, b => b.Id, (a, b) => new
            {
                a,
                b,
            }).Where(x => x.a.Username == username && x.a.Password == password).
            Select(x => new User
            {
                Id = x.a.Id,
                Username = x.a.Username,
                Password = x.a.Password,
                Ho = x.a.Ho,
                Ten = x.a.Ten,
                Email = x.a.Email,
                Sdt = x.a.Sdt,
                AccessToken = x.a.AccessToken,
                Roleid = x.a.Roleid,
                Role = x.b,

            });
            return res.FirstOrDefault();

        }


        public SingleRsp CreateNewUser(User dn)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (common.checkExistbyUserName(dn.Username))
                        {
                            var t = context.User.Add(dn);
                            res.Data = dn;
                            context.SaveChanges();
                            tran.Commit();
                        }
                        else
                        {
                            res.SetMessage("Exist User");

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

        public SingleRsp UpdateUser(User dn)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (!common.checkExistbyID(dn.Id))
                        {
                            var t = context.User.Update(dn);
                            res.Data = dn;
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

        public SingleRsp DeleteUser(User dn)
        {
            var res = new SingleRsp();

            using (var context = new MyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (!common.checkExistbyID(dn.Id))
                        {
                            var t = context.User.Remove(dn);
                            res.Data = dn;
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



        public object findUserPagination(int page, int size, string keyword)
        {
            var pro = All.Where(x => x.Ho.Contains(keyword) || x.Ten.Contains(keyword)).OrderBy(x => x.Roleid);


            var offset = (page - 1) * size;
            var total = pro.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
            var data = pro.OrderBy(x => x.Username).Skip(offset).Take(size).ToList();

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






        private Common common = new Common();


        #endregion


    }
}