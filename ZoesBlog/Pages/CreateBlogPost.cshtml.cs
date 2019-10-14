using System.Threading.Tasks;
using ZoesBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace ZoesBlog.Pages
{
	public class CreateBlogPostModel : PageModel
	{
		private readonly BlogDbContext _blogDbContext;

		public CreateBlogPostModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}

		[BindProperty]
		public BlogPost BlogPost { get; set; }

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

			_blogDbContext.BlogPosts.Add(BlogPost);

			await _blogDbContext.SaveChangesAsync();

			return RedirectToPage("./Index");
		}

		public class UserInputBlogPost
		{
			public string Title { get; set; }
			public string Body { get; set; }
		}
		//public class GeneratedData
		//{
		//	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
			
		//}
		//public string PostReadTime(string text)
		//{
		//	var BlogPost = new BlogPost;
		//	var text = blogPost.Body;

		//	wordCount = text.( "-", " ").Split(" ").Length;
		//	var readingTimeInMinutes = Math.Floor(wordCount / 228) + 1;

		//}
		//blogPost.PostReadTime(BlogPost blogPost);
	}
}
