using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ZoesBlog.Data;

namespace ZoesBlog.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly BlogDbContext _blogDbContext;

		public IReadOnlyCollection<BlogPost> BlogPosts { get; private set; }

		[BindProperty]
		public List<AccessTag> AccessTags { get; private set; }

		public IndexModel(BlogDbContext blogDbContext, ILogger<IndexModel> logger)
		{
			_blogDbContext = blogDbContext;
			_logger = logger;
			AccessTags = new List<AccessTag>();
		}

		public IActionResult OnGet()
		{
			JoinTableData();
			return Page();
		}

		private void JoinTableData()
		{
			AccessTags = _blogDbContext
			   .BlogPosts
			   .OrderByDescending(bp => bp.PublishedAt)
			   .Select(blogPost => new AccessTag(blogPost.Title, blogPost.Body, blogPost.Tags, blogPost.PublishedAt, blogPost.Id)).ToList();
		}

		public class AccessTag
		{
			public readonly string Title;
			public readonly string Body;
			public readonly IReadOnlyCollection<Tag> Tags;
			public readonly DateTime PublishedAt;
			public readonly Guid Id;


			public AccessTag(string title, string body, List<Tag> tags, DateTime publishedAt, Guid id)
			{
				Title = title;
				Body = body;
				Tags = tags;
				PublishedAt = publishedAt;
				Id = id;
			}
		}
	}
}
