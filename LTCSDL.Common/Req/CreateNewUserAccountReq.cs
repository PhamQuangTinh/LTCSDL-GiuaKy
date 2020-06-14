using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{

    public class CreateNewUserAccountReq
    {
        public int Id { get; set; }
        public int Roleid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string Sdt { get; set; }


    }
}
