using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using blog.Data;
using blog.Models;
using blog.Utils;

namespace blog.Pages.BlogUsers
{
    public class CreateModel : PageModel
    {
        private readonly blog.Data.BlogContext _context;

        public CreateModel(blog.Data.BlogContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public SelectList PrivilegeList => new SelectList(EnumList.PrivilegeList);

        [BindProperty]
        public BlogUser BlogUser { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            BlogUser.Password = BlogUser.Password.ToMd5();
            _context.BlogUser.Add(BlogUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
