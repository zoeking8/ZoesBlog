using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ZoesBlog.Data
{
	public class BlogPost
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
	}
}
