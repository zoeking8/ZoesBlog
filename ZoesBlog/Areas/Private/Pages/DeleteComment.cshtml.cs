using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ZoesBlog.Data;

namespace ZoesBlog.Areas.Private.Pages
{
    public class DeleteCommentModel : PageModel
    {
		private readonly BlogDbContext _blogDbContext;

		public DeleteCommentModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}
		[BindProperty]
		public Guid BlogPostId { get; set; }
		[BindProperty]
		public Comment Comments { get; set; }
		public IActionResult OnGet(Guid blogPostId)
		{
			BlogPostId = blogPostId;
			Comments = _blogDbContext.Comments.FirstOrDefault(x => x.Id == blogPostId);
			return Page();
		}
		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			await using (_blogDbContext)
			{
				var comment = _blogDbContext.Comments.FirstOrDefault(c => c.Id == BlogPostId);

				if (comment != null)
				{
					_blogDbContext.Comments.RemoveRange(comment);
					await _blogDbContext.SaveChangesAsync();
				}
			}
			return RedirectToPage("/Index");
		}
	}
}
