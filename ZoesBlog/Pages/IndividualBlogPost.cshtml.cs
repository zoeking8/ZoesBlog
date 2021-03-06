﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZoesBlog.Data;

namespace ZoesBlog.Pages
{
    public class IndividualBlogPostModel : PageModel
	{
		private readonly BlogDbContext _blogDbContext;

		[BindProperty]
		public CommentAccess CommentAccess { get; set; }
		public IReadOnlyCollection<Comment> Comments { get; private set; }
		public IReadOnlyCollection<Tag> Tags { get; private set; }


		public IndividualBlogPostModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}
		public BlogPost BlogPost { get; private set; }
		public async Task<IActionResult> OnGetAsync(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}

			BlogPost = await _blogDbContext.BlogPosts.FirstOrDefaultAsync(bp => bp.Id == id);

			if (BlogPost == null)
			{
				return NotFound();
			}
			var commentList = _blogDbContext.Comments.Where(c => c.BlogPostId == id).ToList();
			commentList.OrderByDescending(bp => bp.PublishedAt);
			Comments = commentList;
			
			var tagList = _blogDbContext.Tags.Where(t => t.BlogPostId == id).ToList();
			Tags = tagList;

			return Page();

		}
		public async Task<IActionResult> OnPostAsync(Guid id)
		{
			_blogDbContext.Comments.Add(new Comment
			{
				BlogPostId = id,
				Body = CommentAccess.Body,
				PublishedAt = DateTime.Now
			});
			await _blogDbContext.SaveChangesAsync();
			return RedirectToPage("./IndividualBlogPost", new {id});
		}
	}
}