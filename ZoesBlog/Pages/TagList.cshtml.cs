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
    public class TagListModel : PageModel
    {
		private readonly BlogDbContext _blogDbContext;

		[BindProperty]
		public Guid BlogPostId { get; set; }
		[BindProperty]
		public string UrlSlug { get; set; }
		public List<BlogPost> BlogPostList { get; private set; }
		public Tag Tags { get; private set; }


		public TagListModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}
		public BlogPost BlogPost { get; private set; }
		public IActionResult OnGet(string urlSlug)
		{
			UrlSlug = urlSlug;
			var blogPosts = _blogDbContext.Tags
				.Where(t => t.UrlSlug == urlSlug)
				.Select(t => t.BlogPost).ToList();

			BlogPostList = blogPosts;
			return Page();

		}
		
	}
}