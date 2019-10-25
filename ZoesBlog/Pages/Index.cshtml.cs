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
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly BlogDbContext _blogDbContext;
		[BindProperty]
		public CommentAccess CommentAccess { get; set; }
		public IReadOnlyCollection<Comment> Comments { get; private set; }


		public PaginatedList<BlogPost> BlogPosts { get; set; }

		

		public IndexModel(BlogDbContext blogDbContext, ILogger<IndexModel> logger)
		{
			_blogDbContext = blogDbContext;
			_logger = logger;
		}

		public async Task OnGetAsync(int? pageIndex)
		{

			IQueryable<BlogPost> blogPostsData = from bp in _blogDbContext.BlogPosts
												 orderby bp.PublishedAt
												 select bp;

			int pageSize = 10;
			PaginatedList<BlogPost> paginatedList = BlogPosts = await PaginatedList<BlogPost>.CreateAsync(
				blogPostsData.AsNoTracking().
				OrderByDescending(bp => bp.PublishedAt), pageIndex ?? 1, pageSize);

			foreach (var blogPost in _blogDbContext.BlogPosts.Include(t => t.Tags))
			{
				return;
			}
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			var blogPost = new BlogPost();
			_blogDbContext.Comments.Add(new Comment
			{
				BlogPostId = blogPost.Id,
				Id = new Guid(),
				Body = CommentAccess.Body,
				PublishedAt = DateTime.UtcNow
			});
			await _blogDbContext.SaveChangesAsync();
			return RedirectToPage("");

		}
	}
}
