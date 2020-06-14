using System;
using System.Collections.Generic;
using System.Text;
using LTCSDL.Common.BLL;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL;
using LTCSDL.DAL.Models;
namespace LTCSDL.BLL
{
    public class RoleSvc : GenericSvc<RoleRep, Role>
    {
        public SingleRsp getAllRole()
        {
            var res = new SingleRsp();
            var m = _rep.getAllRole();
            res.Data = m;
            return res;
        }
    }
}
