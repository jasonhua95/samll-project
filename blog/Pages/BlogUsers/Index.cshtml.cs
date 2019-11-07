using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using blog.Data;
using blog.Models;

namespace blog.Pages.BlogUsers
{
    public class IndexModel : PageModel
    {
        private readonly blog.Data.BlogContext _context;

        public IndexModel(blog.Data.BlogContext context)
        {
            _context = context;
        }

        public IList<BlogUser> BlogUser { get;set; }

        public async Task OnGetAsync()
        {
            BlogUser = await _context.BlogUser.ToListAsync();
        }
    }
}
