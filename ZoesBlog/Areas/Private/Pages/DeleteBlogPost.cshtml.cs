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
    public class DeleteBlogPostModel : PageModel
	{
		private readonly BlogDbContext _blogDbContext;

		public DeleteBlogPostModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}

		[BindProperty]
		public BlogPost BlogPost { get; set; }

		public async Task<IActionResult> OnGetAsync(Guid id)
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

		public async Task<IActionResult> OnPostAsync(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}

			BlogPost = await _blogDbContext.BlogPosts.FindAsync(id);

			if (BlogPost != null)
			{
				_blogDbContext.BlogPosts.Remove(BlogPost);
				await _blogDbContext.SaveChangesAsync();
			}

			return RedirectToPage("./Admin");
		}
	}
}
