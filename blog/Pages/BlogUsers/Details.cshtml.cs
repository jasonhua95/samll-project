﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly blog.Data.BlogContext _context;

        public DetailsModel(blog.Data.BlogContext context)
        {
            _context = context;
        }

        public BlogUser BlogUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BlogUser = await _context.BlogUser.FirstOrDefaultAsync(m => m.Id == id);

            if (BlogUser == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
