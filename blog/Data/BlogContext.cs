using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using blog.Models;

namespace blog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext (DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<Blog> Blog { get; set; }
        public DbSet<BlogTag> BlogTag { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<BlogUser> BlogUser { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Link> Link { get; set; }
        public DbSet<Tag> Tag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogTag>().HasKey(x => new { x.BlogId, x.TagId });
            modelBuilder.Entity<BlogCategory>().HasKey(x => new { x.BlogId, x.CategoryId });
        }
    }
}
