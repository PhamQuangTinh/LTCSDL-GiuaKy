using System;
using System.Collections.Generic;

namespace LTCSDL.DAL.Models
{
    public partial class User
    {
        public User()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Roleid { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string Sdt { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
