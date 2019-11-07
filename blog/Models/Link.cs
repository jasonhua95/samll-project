using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    /// <summary>
    /// 友情链接
    /// </summary>
    public class Link
    {
        public int Id { get; set; }

        [DisplayName("名称")]
        public string Name { get; set; }

        [DisplayName("链接")]
        public string Url { get; set; }

        public int ImageId { get; set; }

        public Image Image { get; set; }

        public int Sort { get; set; }
    }
}
