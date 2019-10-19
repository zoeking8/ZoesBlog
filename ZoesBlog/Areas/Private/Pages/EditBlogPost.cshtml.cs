using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZoesBlog.Data;

namespace ZoesBlog.Areas.Private.Pages
{
	public class EditBlogPostModel : PageModel
	{
		private readonly BlogDbContext _blogDbContext;

		public EditBlogPostModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}

		[BindProperty]
		public BlogPost BlogPost { get; set; }


		public async Task<IActionResult> OnPostAsync(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}

			BlogPost = await _blogDbContext.BlogPosts.FirstOrDefaultAsync(bp => bp.Id == id);

			if (BlogPost == null)
			{
				return NotFound();
			}
			return Page();

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
				if (!_blogDbContext.BlogPosts.Any(bp => bp.Id == BlogPost.Id))
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
	}
}
