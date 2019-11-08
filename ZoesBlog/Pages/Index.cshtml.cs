using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZoesBlog.Data;

namespace ZoesBlog.Pages
{
	public class IndexModel : PageModel
	{
		public HtmlString Tags { get; private set; }

		private readonly ILogger<IndexModel> _logger;
		private readonly BlogDbContext _blogDbContext;

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
			BlogPosts = await PaginatedList<BlogPost>.CreateAsync(
				blogPostsData.AsNoTracking().
				OrderByDescending(bp => bp.PublishedAt), pageIndex ?? 1, pageSize);

			var cloudTags = _blogDbContext.Tags;

			var groupedTags = (from t in cloudTags
							   let urlSlug = t.UrlSlug
							   group t by t.UrlSlug into g
							   select new { UrlSlug = g.Key, Count = g.Count() });
			groupedTags = (from t in groupedTags
						   orderby t.UrlSlug ascending
						   select t);

			double minSize = 10;
			double maxSize = 100;

			double steps = (maxSize - minSize) / groupedTags.Count();

			StringBuilder sb = new StringBuilder();
			foreach (var tag in groupedTags)
			{
				double size = minSize + ((double)tag.Count - 1) * steps;
				sb.Append("<span><a href='./TagList?urlSlug=" + tag.UrlSlug + "' style ='color:black; font-size:" + size + "pt'>" + tag.UrlSlug + "(" + tag.Count + ") </a></span>");
			}
			var sbTags = sb.ToString();
			Tags = new HtmlString(sbTags);
		}
	}
}
