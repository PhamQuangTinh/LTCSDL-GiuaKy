using LTCSDL.Common.DAL;
using LTCSDL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LTCSDL.DAL
{
    public class Common : GenericRep<MyPhamContext, User>
    {
        public Boolean checkExistbyID(int Id)
        {
            var id = All.FirstOrDefault(p => p.Id == Id);
            if (id == null)
            {
                return true;
            }
            return false;
        }

        public Boolean checkExistbyUserName(String username)
        {
            var id = All.FirstOrDefault(p => p.Username == username);
            if (id == null)
            {
                return true;
            }
            return false;
        }



        public Role GetRoleById(int Id)
        {
            var res = Context.Role.FirstOrDefault(p => p.Id == Id);
            return res;
        }

        public Boolean CheckCommentID(int commonetID)
        {
            var comment = Context.Comment.FirstOrDefault(x => x.Id == commonetID);
            if (comment != null) {
                return true;
            }

            return false;
        }



    }
}
