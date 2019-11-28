using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZoesBlog.Data;

namespace ZoesBlog.Seeder
{
	public class DatabaseSeeder
	{
		private BlogDbContext _dbcontext;

		public DatabaseSeeder(BlogDbContext dbContext)
		{
			_dbcontext = dbContext;
		}
		public async Task SeedAsync()
		{
			var blogposts = new List<BlogPost>();
			for (int i = 0; i < 5; i++)
			{
				blogposts.Add(await CreateBlogPostAsync());
			};
			_dbcontext.BlogPosts.AddRange(blogposts);
			await _dbcontext.SaveChangesAsync();

			foreach (var blogPost in blogposts)
			{
				blogPost.Tags = await CreateTagsAsync(blogPost.Id);
				blogPost.Comments = await CreateCommentsAsync(blogPost.Id);
			}
		}

		private async Task<List<Tag>> CreateTagsAsync(Guid blogPostId)
		{
			var _faker = new Faker<Tag>()
				.Rules((f, t) => {
					t.Id = Guid.NewGuid();
					t.Name = f.Random.Word();
					t.BlogPostId = blogPostId;
				});
			List<Tag> newTags = _faker.Generate(5);

			return newTags;
		}

		private async Task<BlogPost> CreateBlogPostAsync()
		{
			var _faker = new Faker<BlogPost>()
				.Rules((f, bp) => {
					bp.Id = Guid.NewGuid();
					bp.Title = f.Random.Words(f.Random.Number(3, 50));
					bp.PublishedAt = f.Date.Past(2);
					bp.Body = f.Rant.Review(f.Commerce.Product());
				});
			var newBlogPost = _faker.Generate();
			return newBlogPost;
		}

		private async Task<List<Comment>> CreateCommentsAsync(Guid blogPostId)
		{
			var _faker = new Faker<Comment>()
				.Rules((f, c) => {
					c.Id = Guid.NewGuid();
					c.Body = f.Random.Words(f.Random.Number(1, 30));
					c.BlogPostId = blogPostId;
				});
			List<Comment> newComments = _faker.Generate(5);

			return newComments;
		}
	}
}
