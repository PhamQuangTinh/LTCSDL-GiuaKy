using System;
using System.Collections.Generic;

namespace LTCSDL.DAL.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public string CommentContent { get; set; }
        public DateTime? TimeComment { get; set; }
    }
}
