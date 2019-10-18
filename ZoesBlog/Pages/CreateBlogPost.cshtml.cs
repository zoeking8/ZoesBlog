﻿using System.Threading.Tasks;
using ZoesBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Collections.Generic;
using Slugify;

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
		public UserInputBlogPost BlogPost { get; set; }
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
				Title = BlogPost.Title,
				Body = BlogPost.Body
			};

			SlugHelper helper = new SlugHelper();

			var tagList = BlogPost.Tags.Split(",")
				.Where(t => !string.IsNullOrEmpty(t));
			var tags = new List<Tag>();

			foreach (var userTag in tagList)
			{
				var tag = new Tag
				{
					BlogPostId = blogPost.Id,
					Name = userTag,
					UrlSlug = helper.GenerateSlug(userTag)
				};
				blogPost.Tags.Add(tag);
			}

			blogPost.Tags = tags;
			_blogDbContext.BlogPosts.Add(blogPost);


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
