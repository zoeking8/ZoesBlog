﻿using System;

namespace ZoesBlog.Data
{
	public class User
	{
		public string Email { get; set; }
		public string Username { get; set; }
		public Guid Id { get; set; }
		public string Password { get; set; }
	}
}
