using System.Threading.Tasks;
using ZoesBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

		public class Foo
		{
			public string Title { get; set; }
			public string Body { get; set; }
		}
	}
}
