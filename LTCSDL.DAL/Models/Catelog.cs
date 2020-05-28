using System;
using System.Collections.Generic;

namespace LTCSDL.DAL.Models
{
    public partial class Catelog
    {
        public Catelog()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
