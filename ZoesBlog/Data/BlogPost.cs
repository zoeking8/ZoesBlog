using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZoesBlog.Data
{
	public class BlogPost
	{
		public Guid Id { get; set; }
		public DateTime PublishedAt { get; set; }
		[Required, StringLength(50, MinimumLength = 3)]
		public string Title { get; set; }
		[Required, StringLength(10000, MinimumLength = 50) ]
		public string Body { get; set; }
		public  List<Tag> Tags { get; set; }
		public List<Comment> Comments { get; set; }
		public double TimeToRead { get; set; }
		public string Snippet { get; set; }
	}
}
