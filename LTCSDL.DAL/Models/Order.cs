using System;
using System.Collections.Generic;

namespace LTCSDL.DAL.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TransactionId { get; set; }
        public int? Quantity { get; set; }
        public string Ghichu { get; set; }

        public virtual Product Product { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
