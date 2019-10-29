using System;

namespace ZoesBlog.Data
{
	public class Comment
	{
		public Guid Id { get; set; }
		public BlogPost BlogPost { get; set; }
		public Guid BlogPostId { get; set; }
		public DateTime PublishedAt { get; set; }
		public string Body { get; set; }
	}
}
