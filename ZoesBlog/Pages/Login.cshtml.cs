using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZoesBlog.Data;

namespace ZoesBlog.Pages
{
	public class LoginModel : PageModel
	{
		private readonly BlogDbContext _blogDbContext;
		public LoginModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}
		[BindProperty]
		public UserAccess userAccess { get; set; }
		
		public async Task<IActionResult> OnPost()
		{
			var user = _blogDbContext.Users.FirstOrDefault(u => u.Email == userAccess.Email);
			if (user == null)
			{
				return Redirect("/Fail"); ;
			}

			bool encryptedPassword = BCrypt.Net.BCrypt.Verify(userAccess.Password, user.Password);

			if (!encryptedPassword)
			{
				return Redirect("/LoginFail");
			}

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, "Administrator")
			};


			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

			return RedirectToPage("/Index", new {area="Private"});

		}
		
	}
}