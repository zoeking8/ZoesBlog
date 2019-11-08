using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZoesBlog.Data;
using ZoesBlog.Pages;

namespace ZoesBlog.Areas.Private.Pages
{
    public class IndexModel : PageModel
    {
		private readonly ILogger<IndexModel> _logger;
		private readonly BlogDbContext _blogDbContext;

		public IndexModel(BlogDbContext blogDbContext, ILogger<IndexModel> logger)
		{
			_blogDbContext = blogDbContext;
			_logger = logger;
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
		}
	}
}
