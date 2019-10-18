using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ZoesBlog.Data;

namespace ZoesBlog.Pages
{
    public class AdminModel : PageModel
    {
		private readonly ILogger<IndexModel> _logger;
		private readonly BlogDbContext _blogDbContext;
		public IReadOnlyCollection<BlogPost> BlogPosts { get; private set; }

		public AdminModel(BlogDbContext blogDbContext, ILogger<IndexModel> logger)
		{
			_blogDbContext = blogDbContext;
			_logger = logger;
		}

		public void OnGet()
        {
			BlogPosts = _blogDbContext.BlogPosts.ToList();
			
			//var newPostFirst = _blogDbContext.BlogPosts.ToList();
			//newPostFirst.Reverse();
			//BlogPosts = newPostFirst;
		}
	}
}