using LTCSDL.Common.BLL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL;
using LTCSDL.DAL.Models;
using Microsoft.Extensions.Options;
using System;

namespace LTCSDL.BLL
{
    public class DangNhapSvc : GenericSvc<DangNhapRep, User>
    {
        #region -- Overrides --
        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        public SingleRsp Remove(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Remove(id);
            res.Data = m;
            return res;
        }
        public SingleRsp findUserByUsername(String userName)
        {
            var res = new SingleRsp();
            var m = _rep.findUserByUsername(userName);
            res.Data = m;
            return res;
        }

        public User findByUserNameAndPassWord(LoginReq req) {
            
            var user = req.Username;
            var pass  = req.Password;
            var lg = _rep.findByUserNameAndPassWord(user, pass);
            return lg;
        }

        public SingleRsp CreateNewUser(CreateNewUserAccountReq req) {
            User dn = new User();
            dn.Id = req.Id;
            dn.Roleid = req.Roleid;
            dn.Ho = req.Ho;
            dn.Ten = req.Ten;
            dn.Email = req.Email;
            dn.Sdt= req.Sdt;
            dn.Username = req.Username;
            dn.Password = req.Password;
            return _rep.CreateNewUser(dn);
        }

        public SingleRsp UpdateUser(CreateNewUserAccountReq req)
        {
            User dn = new User();
            dn.Id = req.Id;
            dn.Roleid = req.Roleid;
            dn.Ho = req.Ho;
            dn.Ten = req.Ten;
            dn.Email = req.Email;
            dn.Sdt = req.Sdt;
            dn.Username = req.Username;
            dn.Password = req.Password;
            return _rep.UpdateUser(dn);
        }

        public SingleRsp RemoveUser(CreateNewUserAccountReq req)
        {
            User dn = new User();
            dn.Id = req.Id;
            dn.Roleid = req.Roleid;
            dn.Ho = req.Ho;
            dn.Ten = req.Ten;
            dn.Email = req.Email;
            dn.Sdt = req.Sdt;
            dn.Username = req.Username;
            dn.Password = req.Password;
            return _rep.DeleteUser(dn);
        }

        public object findUserPagination(int page, int size, string keyword)
        {
            return _rep.findUserPagination(page, size,keyword);
        }




        #endregion

    }
}
