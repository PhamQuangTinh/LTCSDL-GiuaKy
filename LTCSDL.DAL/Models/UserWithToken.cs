using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.DAL.Models
{
    public class UserWithToken : User
    {
        public UserWithToken(User user)
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.Ho = user.Ho;
            this.Ten = user.Ten;
            this.Email = user.Email;
            this.Sdt = user.Sdt;
            this.Role = user.Role;
        }

    }
}
