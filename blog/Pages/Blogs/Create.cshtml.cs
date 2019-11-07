using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace blog.Pages.Blogs
{
    public class CreateModel : PageModel
    {
        private readonly blog.Data.BlogContext _context;

        public CreateModel(blog.Data.BlogContext context)
        {
            _context = context;
        }

        public SelectList selectCategoryItems { get; set; }
        public SelectList selectTagItems { get; set; }

        public IActionResult OnGet()
        {
            selectCategoryItems = new SelectList(_context.Category, "Id", "Name");
            selectTagItems = new SelectList(_context.Tag, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Blog Blog { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Blog.Add(Blog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
