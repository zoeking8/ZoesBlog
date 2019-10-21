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




		//[BindProperty]
		//public List<AccessTag> AccessTags { get; private set; }

		public IndexModel(BlogDbContext blogDbContext, ILogger<IndexModel> logger)
		{
			_blogDbContext = blogDbContext;
			_logger = logger;
			//AccessTags = new List<AccessTag>();
		}

		[BindProperty(SupportsGet = true)]
		public string SearchString { get; set; }
		public string CurrentFilter { get; set; }


		public PaginatedList<BlogPost> BlogPosts { get; set; }

		public async Task OnGetAsync(int? pageIndex, string currentFilter, string searchString)
		{
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
												 orderby bp.PublishedAt
												 select bp;
			if (!string.IsNullOrEmpty(searchString))
			{
				blogPostsData = blogPostsData.Where(bp => bp.Title.Contains(searchString)
									   || bp.Body.Contains(searchString));
			}
			int pageSize = 10;
			PaginatedList<BlogPost> paginatedList = BlogPosts = await PaginatedList<BlogPost>.CreateAsync(
				blogPostsData.AsNoTracking().
				OrderByDescending(bp => bp.PublishedAt), pageIndex ?? 1, pageSize);

			foreach (var blogPost in _blogDbContext.BlogPosts.Include(t => t.Tags))
			{
				return;
			}

			//JoinTableData();
		}


		//private void JoinTableData()
		//{
		//	AccessTags = _blogDbContext
		//	   .BlogPosts
		//	   .OrderByDescending(bp => bp.PublishedAt)
		//	   .Select(blogPost => new AccessTag(blogPost.Title, blogPost.Body, blogPost.Tags, blogPost.PublishedAt, blogPost.Id)).ToList();
		//}



		//public class AccessTag
		//{
		//	public readonly string Title;
		//	public readonly string Body;
		//	public readonly IReadOnlyCollection<Tag> Tags;
		//	public readonly DateTime PublishedAt;
		//	public readonly Guid Id;


		//	public AccessTag(string title, string body, List<Tag> tags, DateTime publishedAt, Guid id)
		//	{
		//		Title = title;
		//		Body = body;
		//		Tags = tags;
		//		PublishedAt = publishedAt;
		//		Id = id;
		//	}

		//}

	}
	
}
