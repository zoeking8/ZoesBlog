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

		
		public IReadOnlyCollection<BlogPost> BlogPosts { get; private set; }


		[BindProperty]
		public List<AccessTag> AccessTags { get; private set; }

		public IndexModel(BlogDbContext blogDbContext, ILogger<IndexModel> logger)
		{
			_blogDbContext = blogDbContext;
			_logger = logger;
			AccessTags = new List<AccessTag>();
		}


		public string TitleSort { get; set; }
		public string DateSort { get; set; }
		public string CurrentFilter { get; set; }
		public string CurrentSort { get; set; }

		public PaginatedList<BlogPost> PageBlogPosts { get; set; }
		public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
		{
			CurrentSort = sortOrder;
			TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
			DateSort = sortOrder == "Date" ? "date_desc" : "Date";
			if (searchString != null)
			{
				pageIndex = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			CurrentFilter = searchString;

			IQueryable<BlogPost> blogPostsData = from bp in _blogDbContext.BlogPosts
												 select bp;

			if (!String.IsNullOrEmpty(searchString))
			{
				blogPostsData = blogPostsData.Where(bp => bp.Title.Contains(searchString)
									   || bp.Body.Contains(searchString));
			}
			switch (sortOrder)
			{
				case "title_desc":
					blogPostsData = blogPostsData.OrderByDescending(bp => bp.Title);
					break;
				case "Date":
					blogPostsData = blogPostsData.OrderBy(bp => bp.PublishedAt);
					break;
				case "date_desc":
					blogPostsData = blogPostsData.OrderByDescending(bp => bp.PublishedAt);
					break;
				default:
					blogPostsData = blogPostsData.OrderBy(bp => bp.Title);
					break;
			}

			int pageSize = 3;
			PageBlogPosts = await PaginatedList<BlogPost>.CreateAsync(
				blogPostsData.AsNoTracking(), pageIndex ?? 1, pageSize);
			JoinTableData();
			//return Page();
		}






		//public IActionResult OnGet()
		//{
		//	JoinTableData();
		//	return Page();
		//}


		private void JoinTableData()
		{
			AccessTags = _blogDbContext
			   .BlogPosts
			   //.OrderByDescending(bp => bp.PublishedAt)
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
