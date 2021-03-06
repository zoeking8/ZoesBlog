using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Slugify;
using ZoesBlog.Data;
namespace ZoesBlog.Areas.Private.Pages
{
	public class CreateBlogPostModel : PageModel
	{
		private readonly BlogDbContext _blogDbContext;
		public CreateBlogPostModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}
		[BindProperty]
		public UserInputBlogPost UserBlogPost { get; set; }
		public IActionResult OnGet()
		{
			return Page();
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			var blogPost = new BlogPost
			{
				PublishedAt = DateTime.UtcNow,
				Title = UserBlogPost.Title,
				Body = UserBlogPost.Body,
			};
			var wordCount = blogPost.Body.Split(" ").Length;
			var readingTimeInMinutes = Math.Floor(wordCount / 228d) + 1;
			blogPost.TimeToRead = readingTimeInMinutes;

			blogPost.Snippet = string.Join(" ", blogPost.Body.Split().Take(150).Append("..."));

			var tagList = new string[] { };
			if (!string.IsNullOrEmpty(UserBlogPost.Tags))
			{
				tagList = UserBlogPost.Tags.Split(",");
			}
			SlugHelper helper = new SlugHelper();
			var tags = tagList.Select(userTag => new Tag { BlogPostId = blogPost.Id, Name = userTag, UrlSlug = helper.GenerateSlug(userTag) }).ToList();
			blogPost.Tags = tags;

			_blogDbContext.BlogPosts.Add(blogPost);

			await _blogDbContext.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
		public class UserInputBlogPost
		{
			[Required]
			public string Title { get; set; }
			[Required]
			public string Body { get; set; }
			public string Tags { get; set; }
		}
	}
}