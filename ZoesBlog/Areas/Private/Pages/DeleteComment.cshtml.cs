using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
		public Comment Comment { get; set; }

		public async Task<IActionResult> OnGetAsync(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Comment = await _blogDbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

			if (Comment == null)
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

			Comment = await _blogDbContext.Comments.FindAsync(id);

			if (Comment != null)
			{
				_blogDbContext.Comments.Remove(Comment);
				await _blogDbContext.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
