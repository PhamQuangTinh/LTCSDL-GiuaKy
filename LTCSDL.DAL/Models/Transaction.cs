using System;
using System.Collections.Generic;

namespace LTCSDL.DAL.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TimeTransaction { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
