using System;
using System.Linq;
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
							Username = "ZoeKing",
							Password = BCrypt.Net.BCrypt.HashPassword("Zoe1811")
						},
						new User
						{
							Id = Guid.NewGuid(),
							Email = "zoeking@live.co.uk",
							Username = "ZoeKing2",
							Password = BCrypt.Net.BCrypt.HashPassword("Zoe1811")
						}
					); 
				}
				context.SaveChanges();
			}
		}
	}
}
