using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZoesBlog.Data;

namespace ZoesBlog.Pages
{

	public class DeleteBlogPostModel : PageModel
    {
		private readonly BlogDbContext _blogDbContext;

		public DeleteBlogPostModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}

		[BindProperty]
		public BlogPost BlogPost { get; set; }

		public IActionResult OnGet()
		{
			return Page();
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_blogDbContext.BlogPosts.Remove(BlogPost);

			await _blogDbContext.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}