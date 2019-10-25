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
					logger.LogError(ex, "An error occurred seeding the DB.");
				}
			}

			var configuration = new ConfigurationBuilder()
							   .AddJsonFile("appsettings.json")
							   .Build();
			var setting = configuration.GetValue<string>("Logging:LogLevel:Default");

			Log.Logger = new LoggerConfiguration()
		   .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
		   .Enrich.WithProperty("Name","Zoe")
		   .Enrich.WithProperty("Environment", configuration.GetValue<string>("Environment"))
		   .Enrich.WithProperty("Component", configuration.GetValue<string>("Component"))
		   .ReadFrom.Configuration(configuration)
		   .Enrich.FromLogContext()
		   .WriteTo.Console()
		  .Enrich.FromLogContext()
		   .WriteTo.Seq(serverUrl: configuration.GetValue<string>("Logging:Seq:Url"),
									   apiKey: configuration.GetValue<string>("Logging:Seq:ApiKey"))
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
