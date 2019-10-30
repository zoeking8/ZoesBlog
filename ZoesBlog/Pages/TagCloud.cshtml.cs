using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sparc.TagCloud;
using ZoesBlog.Data;

namespace ZoesBlog.Pages
{
    public class TagCloudModel : PageModel
    {
		private readonly BlogDbContext _blogDbContext;

		public Tag Tag { get; private set; }
		public string UrlSlug { get; set; }
		public List<BlogPost> BlogPosts { get; set; }
		public List<TagCloudTag> TagCloud { get; private set; }
		public TagCloudModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}

		
		private List<TagCloudTag> GenerateTagCloud()
		{
			var analyzer = new TagCloudAnalyzer();

			var blogPostTags = _blogDbContext
							   .Tags
							   .Select(t => t.Name)
							   .ToList();


			var tags = analyzer.ComputeTagCloud(blogPostTags);

			tags = tags.Shuffle();
			return tags.ToList();
		}
		public IActionResult OnGet()
		{
			TagCloud = GenerateTagCloud();
			return Page();

		}

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