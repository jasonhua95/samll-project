using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment
    {
        public int Id { get; set; }

        public int BlogUserId { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }

        public BlogUser BlogUser { get; set; }

        public Blog Blog { get; set; }
    }
}
