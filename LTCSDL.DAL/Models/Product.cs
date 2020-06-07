using System;
using System.Collections.Generic;

namespace LTCSDL.DAL.Models
{
    public partial class Product
    {
        public Product()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int CatelogId { get; set; }
        public string Productname { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Productcontent { get; set; }
        public int ProductInventory { get; set; }
        public string ProductImgLink { get; set; }

        public virtual Catelog Catelog { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
