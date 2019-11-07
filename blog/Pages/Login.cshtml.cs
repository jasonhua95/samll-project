using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Models;
using blog.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace blog.Pages
{
    public class LoginModel : PageModel
    {
        private readonly blog.Data.BlogContext _context;

        public LoginModel(blog.Data.BlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BlogUser BlogUser { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPostAsync()
        {
            if (string.IsNullOrEmpty(BlogUser.Name) || string.IsNullOrEmpty(BlogUser.Password))
            {
                Message = "用户名或者密码错误";
                return Page();
            }
            var login = _context.BlogUser.FirstOrDefault(x => x.Name.ToLower() == BlogUser.Name.ToLower() && x.Password == BlogUser.Password.ToMd5());
            if (login == null) {
                Message = "用户名或者密码错误";
                return Page();
            }
            return RedirectToPage("./Index");
        }

    }
}