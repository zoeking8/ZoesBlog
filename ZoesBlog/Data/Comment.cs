using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZoesBlog.Data
{
	public class Comment
	{
		public Guid Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Body { get; set; }
		
	}
}
