using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blog.Data;
using blog.Models;
using blog.Utils;

namespace blog.Pages.BlogUsers
{
    public class EditModel : PageModel
    {
        private readonly blog.Data.BlogContext _context;

        public EditModel(blog.Data.BlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BlogUser BlogUser { get; set; }

        public SelectList PrivilegeList => new SelectList(EnumList.PrivilegeList);

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
            BlogUser.Password = null;
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BlogUser).State = EntityState.Modified;

            try
            {
                BlogUser.Password = BlogUser.Password.ToMd5();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogUserExists(BlogUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BlogUserExists(int id)
        {
            return _context.BlogUser.Any(e => e.Id == id);
        }
    }
}
