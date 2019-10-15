using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ZoesBlog.Data
{
	public class BlogPost
	{
		public Guid Id { get; set; }
		public DateTime PublishedAt { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		//public string TimeToRead { get; set; }
		//public string Snippit { get; set; }
		//public List<Tag> Tags { get; set; }
		//public List<Comment> Comments { get; set; }
	}
}
