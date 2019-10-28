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
    public class ViewCommentsModel : PageModel
    {
		
		private readonly BlogDbContext _blogDbContext;

		[BindProperty]
		public Guid BlogPostId { get; set; }
		[BindProperty]
		public CommentAccess CommentAccess { get; set; }
		public IReadOnlyCollection<Comment> Comments { get; private set; }

		public ViewCommentsModel(BlogDbContext blogDbContext)
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
			var commentList = _blogDbContext.Comments.Where(x => x.BlogPostId == id).ToList();
			commentList.Reverse();
			Comments = commentList;

			return Page();
		}
			
	}
}
