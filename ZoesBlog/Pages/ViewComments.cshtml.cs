using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZoesBlog.Data;

namespace ZoesBlog.Pages
{
    public class ViewCommentsModel : PageModel
    {
		private readonly BlogDbContext _blogDbContext;

		public ViewCommentsModel(BlogDbContext blogDbContext)
		{
			_blogDbContext = blogDbContext;
		}

		[BindProperty]
		public CommentAccess CommentAccess { get; set; }
		public IReadOnlyCollection<Comment> Comments { get; private set; }
		
	}
}