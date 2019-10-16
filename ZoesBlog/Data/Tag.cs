using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZoesBlog.Data
{
	public class Tag
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public BlogPost blogPost { get; set; }
		public Guid BlogPostId { get; set; }
	}
}
