using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZoesBlog.Data;
using Microsoft.AspNetCore.Html;
namespace ZoesBlog.Pages
{
	public class TagCloudModel : PageModel
    {
		public HtmlString Tags { get; private set; }
		private readonly BlogDbContext _blogDbContext;
		public IReadOnlyCollection<Tag> Tag { get; private set; }

		[BindProperty]
		public string UrlSlug { get; set; }

		public TagCloudModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}
		public IActionResult OnGet()
		{
			var CloudTags = _blogDbContext.Tags;

			var groupedTags = (from t in CloudTags
						group t by t.Name into g
						select new { Name = g.Key, Count = g.Count() });
			groupedTags = (from t in groupedTags
						   orderby t.Name ascending
						   select t);

			double minSize = 10;
			double maxSize = 100;

			double steps = (maxSize - minSize) / groupedTags.Count();

			StringBuilder sb = new StringBuilder();
			foreach (var tag in groupedTags)
			{
				double size = minSize + ((double)tag.Count - 1) * steps;
				sb.Append("<span><a asp-page='./TagList' asp-route-urlSlug='(" + tag.Name + ")'style ='font-size:" + size + "pt'>" + tag.Name + "(" + tag.Count + ") </a></span>");
			}
			var sbTags = sb.ToString();
			Tags = new HtmlString(sbTags);
			return Page();
			//sb.Append("<span style='font-size:" + size + "pt'>" + tag.Name + "(" + tag.Count + ") </span>");

		}
	}
}