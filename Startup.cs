using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IIS.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IIS
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAny", builder =>
				{
					builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				});
			});

			services.AddControllersWithViews();
			services.AddTransient<ToDoService>();
			services.AddSingleton(new DatabaseOptions()
				{ConnectionString = Configuration["ConnectionString"]});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			DefaultFilesOptions defaultFiles = new DefaultFilesOptions();
			defaultFiles.DefaultFileNames.Clear();
			defaultFiles.DefaultFileNames.Add("index.html");
			app.UseDefaultFiles(defaultFiles);
			app.UseStaticFiles();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}			

			app.UseRouting();

			app.UseCors("AllowAny");

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
