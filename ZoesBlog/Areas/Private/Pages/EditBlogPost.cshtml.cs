using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Slugify;
using ZoesBlog.Data;

namespace ZoesBlog.Areas.Private.Pages
{
	public class EditBlogPostModel : PageModel
	{
		private readonly BlogDbContext _blogDbContext;
		[BindProperty]
		public UserInputBlogPost UserBlogPost { get; set; }
		public EditBlogPostModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}
		[BindProperty]
		public Guid BlogPostId { get; set; }
		[BindProperty]
		public BlogPost BlogPost { get; set; }
		[BindProperty]
		public string Tags { get; set; }

		public IActionResult OnGet(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}
			BlogPostId = id;

			BlogPost = _blogDbContext.BlogPosts.FirstOrDefault(bp => bp.Id == id);

			var tags = _blogDbContext.Tags
				.Where(t => t.BlogPostId == id)
				.ToList();


			var listOfTags = new StringBuilder();
				foreach (var tag in tags)
				{
					listOfTags.Append(tag.Name + ", ");
				}
			;
			
			Tags = listOfTags.ToString().Remove(listOfTags.Length-2);

			if (BlogPost == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPost(Guid id)
		{
			{
			if (!ModelState.IsValid)
			
				return Page();
			}

			_blogDbContext.Entry(BlogPost).Property(bp => bp.Title).IsModified = true;
			_blogDbContext.Entry(BlogPost).Property(bp => bp.Body).IsModified = true;
			_blogDbContext.Entry(BlogPost).Collection(bp => bp.Tags).IsModified = true;
			_blogDbContext.Entry(BlogPost).Property(bp => bp.PublishedAt).IsModified = true;
			_blogDbContext.Entry(BlogPost).Property(bp => bp.Snippet).IsModified = true;
			_blogDbContext.Entry(BlogPost).Property(bp => bp.TimeToRead).IsModified = true;
			_blogDbContext.Entry(BlogPost).Property(bp => bp.Id).IsModified = false;

			var tagsDelete = _blogDbContext.Tags
				.Where(t => t.BlogPostId == id);
			_blogDbContext.Tags.RemoveRange(tagsDelete);

			BlogPost.PublishedAt = DateTime.UtcNow;

			BlogPost.Snippet = string.Join(" ", BlogPost.Body.Split().Take(150).Append("..."));
			
			var wordCount = BlogPost.Body.Split(" ").Length;
			var readingTimeInMinutes = Math.Floor(wordCount / 228d) + 1;
			BlogPost.TimeToRead = readingTimeInMinutes;

			BlogPostId = id;
			var tagList = new string[] { };
			if (!string.IsNullOrEmpty(UserBlogPost.Tags))
			{
				tagList = UserBlogPost.Tags.Split(",");
			}
			SlugHelper helper = new SlugHelper();
			var tags = tagList.Select(userTag => new Tag { BlogPostId = BlogPost.Id, Name = userTag, UrlSlug = helper.GenerateSlug(userTag) }).ToList();

			_blogDbContext.Tags.UpdateRange(tags);
			await _blogDbContext.SaveChangesAsync();
			
			return RedirectToPage("./Index");
		}
		public class UserInputBlogPost
		{
			public string Title { get; set; }
			public string Body { get; set; }
			public string Tags { get; set; }
		}
	}
}
