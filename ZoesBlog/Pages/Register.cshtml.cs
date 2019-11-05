using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZoesBlog.Data;

namespace ZoesBlog.Pages
{
    public class RegisterModel : PageModel
    {
		private readonly BlogDbContext _blogDbContext;

		public RegisterModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}
		[BindProperty]
		public RegisterAccess RegisterAccess { get; set; }

		public IActionResult OnGet()
		{
			return Page();
		}
		private string HashPassword(string password)
		{
			var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);
			return encryptedPassword;
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			_blogDbContext.Users.Add(new User
			{
				Username = RegisterAccess.Username,
				Email = RegisterAccess.Email,
				Password = HashPassword(RegisterAccess.Password)
			});
			await _blogDbContext.SaveChangesAsync();
			return RedirectToPage("./Index");
		}

		
	}

	public class RegisterAccess
	{
		public string Username { get;  set; }
		public string Email { get;  set; }
		public string Password { get;  set; }
	}
}