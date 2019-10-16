using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ZoesBlog.Data
{
	public static class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new BlogDbContext(serviceProvider.GetRequiredService<DbContextOptions<BlogDbContext>>()))
			{
				if (!context.BlogPosts.Any())
				{
					context.BlogPosts.AddRange
					(
						new BlogPost
						{
							Id = Guid.NewGuid(),
							Title = "Hello World!",
							Body = "This is my first blog post.",
							PublishedAt = DateTime.UtcNow
						}
					);
				}

				context.SaveChanges();
			}
		}
	}
}
