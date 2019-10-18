using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZoesBlog.Areas.Identity.Data;
using ZoesBlog.Models;

[assembly: HostingStartup(typeof(ZoesBlog.Areas.Identity.IdentityHostingStartup))]
namespace ZoesBlog.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ZoesBlogContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ZoesBlogContextConnection")));

                services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ZoesBlogContext>();
            });
        }
    }
}