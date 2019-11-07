using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    /// <summary>
    /// 种类
    /// </summary>
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BlogCategory> Blogs { get; set; }
    }
}
