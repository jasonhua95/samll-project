using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    /// <summary>
    /// 博客
    /// </summary>
    public class Blog
    {
        public int Id { get; set; }
        public int BlogUserId { get; set; }

        [DisplayName("标题")]
        public string Title { get; set; }

        [DisplayName("内容")]
        public string Content { get; set; }
        [DisplayName("时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        [DisplayName("点击量")]
        public int Hits { get; set; }

        [DisplayName("创建者")]
        public BlogUser BlogUser { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [DisplayName("标签")]
        public ICollection<BlogTag> Tags { get; set; }

        /// <summary>
        /// 种类
        /// </summary>
        [DisplayName("种类")]
        public ICollection<BlogCategory> Categories { get; set; }

        [DisplayName("图片")]
        public ICollection<Image> Images { get; set; }
    }

    /// <summary>
    /// 一个博客有多个标签，一个种类有多个标签，博客和标签多对多，链接表
    /// </summary>
    public class BlogTag
    {
        public int BlogId { get; set; }
        public int TagId { get; set; }

        public Blog Blog { get; set; }

        public Tag Tag { get; set; }
    }

    /// <summary>
    /// 一个博客有多个种类，一个种类有多个博客，博客和种类多对多，链接表
    /// </summary>
    public class BlogCategory
    {
        public int BlogId { get; set; }

        public int CategoryId { get; set; }

        public Blog Blog { get; set; }

        public Category Category { get; set; }
    }
}
