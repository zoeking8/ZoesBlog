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
	public class IndividualBlogPostModel : PageModel
	{
		private readonly BlogDbContext _blogDbContext;

		public string Title { get; set; }
		public Guid BlogPostId { get; set; }

		public IndividualBlogPostModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}
		public BlogPost BlogPost { get; private set; }
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
	}
}
