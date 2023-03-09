using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission09_ea11160.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_ea11160
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }

        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // use the mvc pattern
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]);
            });

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();

            // allow us to use razor pages
            services.AddRazorPages();

            // allow for sessions
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // connect with wwwroot
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // map a way back to the home page after filtering by category... should i make home a category?
                //endpoints.MapControllerRoute(
                //    name: "home",
                //    pattern: "",
                //    defaults: new { controller = "Home", action = "Index" });

                // put them in the order you want them to read in, most specific first to least specific
                endpoints.MapControllerRoute(
                    name: "categorypage",
                    pattern: "{bookCategory}/Page{pageNum}",
                    defaults: new { Controller = "Home", Action = "Index" }
                    );

                // don't have to include the name, pattern, defaults but that is the order
                endpoints.MapControllerRoute("Paging", "Page{pageNum}", new { Controller = "Home", Action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute("category", "{bookCategory}", new { Controller = "Home", Action = "Index", pageNum = 1 });
                
                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });
        }
    }
}
