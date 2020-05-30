using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class RefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
