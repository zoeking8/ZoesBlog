using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZoesBlog.Data;

namespace ZoesBlog.Pages
{
    public class TagListModel : PageModel
    {
		private readonly BlogDbContext _blogDbContext;

		[BindProperty]
		public string UrlSlug { get; set; }
		public List<BlogPost> BlogPostList { get; private set; }

		public TagListModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}
		public IActionResult OnGet(string urlSlug)
		{
			UrlSlug = urlSlug;
			var blogPosts = _blogDbContext.Tags
				.Where(t => t.UrlSlug == urlSlug)
				.Select(t => t.BlogPost)
				.OrderByDescending(t => t.PublishedAt).ToList();
			BlogPostList = blogPosts;

			return Page();
		}
	}
}