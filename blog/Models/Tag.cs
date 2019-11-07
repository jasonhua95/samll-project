using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    /// <summary>
    /// 标签
    /// </summary>
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BlogTag> Blogs { get; set; }
    }
}
