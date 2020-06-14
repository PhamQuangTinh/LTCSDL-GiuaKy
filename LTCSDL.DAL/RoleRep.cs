using LTCSDL.Common.DAL;
using LTCSDL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LTCSDL.DAL
{
    public class RoleRep : GenericRep<MyPhamContext, Role>
    {
        public List<Role> getAllRole()
        {
            var res = All.Select(x => x).ToList();
            return res;
        }
    }
}
