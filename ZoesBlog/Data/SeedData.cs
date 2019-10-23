using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Slugify;

namespace ZoesBlog.Data
{
	public static class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new BlogDbContext(serviceProvider.GetRequiredService<DbContextOptions<BlogDbContext>>()))
			{
				SlugHelper helper = new SlugHelper();

				if (!context.Users.Any())
				{
					context.Users.AddRange
					(
						new User
						{
							Id = Guid.NewGuid(),
							Email = "zoe.king@razor.co.uk",
							Username = "Zoe1",
							Password = BCrypt.Net.BCrypt.HashPassword("Zoe1811")
						},
						new User
						{
							Id = Guid.NewGuid(),
							Email = "zoeking@live.co.uk",
							Username = "Zoe2",
							Password = BCrypt.Net.BCrypt.HashPassword("Zoe1811")
						}

					); 
				}

				if (!context.BlogPosts.Any())
				{
					context.BlogPosts.AddRange
					(
						new BlogPost
						{
							Id = Guid.NewGuid(),
							Title = "Hello World!",
							Body = "This is my first blog post weeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeweeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee.",
							PublishedAt = DateTime.UtcNow,
							Tags = new List<Tag>{new Tag
							{
							Id = Guid.NewGuid(),
							Name = "Tag Test",
							UrlSlug = helper.GenerateSlug("Tag Test")

						}   }
						}
						
					);
				}

				context.SaveChanges();
			}
		}
	}
}
