using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace blog.Pages
{
    public class BTModel : PageModel
    {
        private readonly blog.Data.BlogContext _context;

        public BTModel(blog.Data.BlogContext context)
        {
            _context = context;
        }

        public SelectList selectCategoryItems { get; set; }
        public SelectList selectTagItems { get; set; }

        public void OnGet()
        {

        }
    }
}