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
    public class DeleteModel : PageModel
    {
        private readonly blog.Data.BlogContext _context;

        public DeleteModel(blog.Data.BlogContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BlogUser = await _context.BlogUser.FindAsync(id);

            if (BlogUser != null)
            {
                _context.BlogUser.Remove(BlogUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
