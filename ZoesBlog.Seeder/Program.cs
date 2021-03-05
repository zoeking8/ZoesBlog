using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ZoesBlog.Data;

namespace ZoesBlog.Seeder
{
	class Program
	{
		static void Main(string[] args)
		{
			MainAsync().Wait();
		}

		private static async Task MainAsync()
		{
			var sqlServerConnectionString = PromptSqlServerConnectionString();
			var blogDbContext = ConfigureApplicationDbContext(sqlServerConnectionString);
			var numberOfBlogPosts = PromptBlogPostNumber();
			//await DatabaseSeeder.SeedAsync();
		}

		private static int PromptBlogPostNumber()
		{
			string defaultBlogPostNumber = "5";
			Console.WriteLine("How many Blog Posts would you like to generate?");
			var blogPostNumber = Console.ReadLine() ?? defaultBlogPostNumber;
			return Int32.Parse(blogPostNumber);
		}

		private static string PromptSqlServerConnectionString()
		{
			var defaultSqlServerConnectionString = "Server=.; Database=ZoesBlog; Integrated Security=True;";
			Console.WriteLine("Enter your connection string");
			//return Console.ReadLine();
			return Console.ReadLine() ?? defaultSqlServerConnectionString;
		}
		private static BlogDbContext ConfigureApplicationDbContext(string sqlServerConnectionString)
		{
			var blogDbContextOptionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
			blogDbContextOptionsBuilder.UseSqlServer(sqlServerConnectionString);
			return new BlogDbContext(blogDbContextOptionsBuilder.Options);
		}
	}
}
