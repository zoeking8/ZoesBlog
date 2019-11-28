using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZoesBlog.Data;

namespace ZoesBlog.Areas.Private.Pages
{
	public class EditBlogPostModel : PageModel
	{
		private readonly BlogDbContext _blogDbContext;

		public EditBlogPostModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}

		[BindProperty]
		public BlogPost BlogPost { get; set; }
		[BindProperty]
		public Guid BlogPostId { get; set; }

		[BindProperty]
		public string TagsList { get; set; }
		[BindProperty]
		public string Tags { get; set; }

		public async Task<IActionResult> OnGetAsync(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}

			BlogPost = await _blogDbContext.BlogPosts.FirstOrDefaultAsync(bp => bp.Id == id);
			var listOfTags = _blogDbContext.Tags.Where(t => t.BlogPostId == id).Select(t => t.Name).ToList();
			Tags = listOfTags.ToString();
			if (BlogPost == null)
			{
				return NotFound();
			}
			return Page();

		}


		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_blogDbContext.Entry(BlogPost).Property(bp => bp.Title).IsModified = true;
			_blogDbContext.Entry(BlogPost).Property(bp => bp.Body).IsModified = true;
			_blogDbContext.Entry(BlogPost).Collection(bp => bp.Tags).IsModified = true;
			_blogDbContext.Entry(BlogPost).Property(bp => bp.PublishedAt).IsModified = false;
			_blogDbContext.Entry(BlogPost).Property(bp => bp.Snippet).IsModified = false;
			_blogDbContext.Entry(BlogPost).Property(bp => bp.TimeToRead).IsModified = false;
			_blogDbContext.Entry(BlogPost).Property(bp => bp.Id).IsModified = false;
			_blogDbContext.Attach(Tags).State = EntityState.Modified;
			BlogPost.PublishedAt = DateTime.UtcNow;

			BlogPost.Snippet = string.Join(" ", BlogPost.Body.Split().Take(150).Append("..."));
			
			var wordCount = BlogPost.Body.Split(" ").Length;
			var readingTimeInMinutes = Math.Floor(wordCount / 228d) + 1;
			BlogPost.TimeToRead = readingTimeInMinutes;

			try
			{
				await _blogDbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_blogDbContext.BlogPosts.Any(bp => bp.Id == BlogPost.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return RedirectToPage("./Index");

		}

	}
}
