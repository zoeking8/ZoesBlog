using System;

namespace ZoesBlog.Data
{
	public class Tag
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public BlogPost BlogPost { get; set; }
		public Guid BlogPostId { get; set; }
		public string UrlSlug { get; set; }
	}
}
