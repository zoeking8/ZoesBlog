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
using Serilog.Sinks;


namespace ZoesBlog
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
		   .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
		   .Enrich.FromLogContext()
		   .WriteTo.Console()
		   .WriteTo.Seq(
				Environment.GetEnvironmentVariable("SEQ_URL") ?? "http://localhost:5341")
		   .CreateLogger();

			try
			{
				Log.Information("Starting up");
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Application start-up failed");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.UseSerilog()
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});

	}
}
