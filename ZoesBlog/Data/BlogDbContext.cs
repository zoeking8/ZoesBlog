﻿using Microsoft.EntityFrameworkCore;


namespace ZoesBlog.Data
{
	public class BlogDbContext : DbContext
	{
		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Comment> Comments { get; set; }

		public BlogDbContext(DbContextOptions<BlogDbContext> options)
			: base(options)
		{

		}
	}
}
