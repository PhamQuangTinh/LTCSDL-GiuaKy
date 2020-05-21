using System;
using System.Collections.Generic;

namespace LTCSDL.DAL.Models
{
    public partial class SanPham
    {
        public int Id { get; set; }
        public string TenSanPham { get; set; }
        public string Gia { get; set; }
        public string MoTa { get; set; }
        public string ChucNang { get; set; }
    }
}
