using System;
using System.Collections.Generic;

namespace LTCSDL.DAL.Models
{
    public partial class Role
    {
        public Role()
        {
            Dangnhap = new HashSet<Dangnhap>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Dangnhap> Dangnhap { get; set; }
    }
}
