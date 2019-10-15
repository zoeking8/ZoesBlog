using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZoesBlog.Data;

namespace ZoesBlog.Pages
{
	public class EditBlogPostModel : PageModel
	{
		[BindProperty]
		public BlogPost BlogPost { get; set; }

		private readonly BlogDbContext _blogDbContext;
		private readonly ILogger<EditBlogPostModel> _logger;

		public EditBlogPostModel(BlogDbContext blogDbContext, ILogger<EditBlogPostModel> logger)
		{
			_blogDbContext = blogDbContext;
			_logger = logger;
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_blogDbContext.Attach(BlogPost).State = EntityState.Modified;

			try
			{
				await _blogDbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BlogPostExists(BlogPost.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Admin");
		}

		private bool BlogPostExists(Guid id)
		{
			return _blogDbContext.BlogPosts.Any(b => b.Id == id);
		}
	}
}