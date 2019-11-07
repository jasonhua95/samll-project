using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    /// <summary>
    /// 图片
    /// </summary>
    public class Image
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }
    }
}
