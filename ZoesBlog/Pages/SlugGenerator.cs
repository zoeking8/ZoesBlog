using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZoesBlog.Pages
{
	public static class SlugGenerator
	{
		public static string GenerateSlug(this string phrase)
		{
			string str = phrase.ToLower();

			str = Regex.Replace(str, @"[^a-z0-9\s-]", "");          
			str = Regex.Replace(str, @"\s+", " ").Trim();
			str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();   
			str = Regex.Replace(str, @"\s", "-");  

			return str;
		}
	}
}
