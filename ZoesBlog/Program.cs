using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZoesBlog.Data;
using Serilog;
using Serilog.Events;

namespace ZoesBlog
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					SeedData.Initialize(services);
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occured seeding the DB.");
				}
			}
			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
					//webBuilder.UseSerilog((hostingContext, loggerConfiguration) =>

					//loggerConfiguration
					//		.Enrich.FromLogContext()
					//		.MinimumLevel.Is(hostingContext.Configuration.GetValue<LogEventLevel>("Serilog: LogEventLevel")));

					//var seqUrl = "localhost:5341";

					//loggerConfiguration.WriteTo.Seq;
					//(
					//	seqUrl,
					//	apiKey:hostingContext.Configuration.GetValue
					//)
				});

	}
}
