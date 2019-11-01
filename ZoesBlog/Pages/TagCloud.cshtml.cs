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
						   orderby t.Name descending
						   select t);

			double minSize = 10;
			double maxSize = 100;

			double steps = (maxSize - minSize) / groupedTags.Count();

			StringBuilder sb = new StringBuilder();
			foreach (var tag in groupedTags)
			{
				double size = minSize + ((double)tag.Count - 1) * steps;
				sb.Append("<span style='font-size:" + size + "pt'>" + tag.Name + "(" + tag.Count + ") </span>");
			}
			var sbTags = sb.ToString();
			Tags = new HtmlString(sbTags);
			return Page();
			//"<a asp-page='./ TagList" + asp-route-urlSlug="("tag.UrlSlug")" class="label label - default">@tag.UrlSlug</a>
		}






















		//private readonly BlogDbContext _blogDbContext;

		//public Tag Tag { get; private set; }
		//public string UrlSlug { get; set; }
		//public List<BlogPost> BlogPosts { get; set; }
		//public List<TagCloudTag> TagCloud { get; private set; }
		//public TagCloudModel(BlogDbContext blogDbContext)
		//{
		//	_blogDbContext = blogDbContext;
		//}


		//private List<TagCloudTag> GenerateTagCloud()
		//{
		//	var analyzer = new TagCloudAnalyzer();

		//	var blogPostTags = _blogDbContext
		//					   .Tags
		//					   .Select(t => t.Name)
		//					   .ToList();


		//	var tags = analyzer.ComputeTagCloud(blogPostTags);

		//	tags = tags.Shuffle();
		//	return tags.ToList();
		//}
		//public IActionResult OnGet()
		//{
		//	TagCloud = GenerateTagCloud();
		//	return Page();

		//}

		//public void OnGet()
		//      {
		//	var analyzer = new TagCloudAnalyzer();

		//	// blogPosts is an IEnumerable<String>, loaded from
		//	// the database or whatevz.
		//	var tags = analyzer.ComputeTagCloud(blogPosts);

		//	// Shuffle the tags, if you like for a random
		//	// display
		//	tags = tags.Shuffle();
		//}
	}
}