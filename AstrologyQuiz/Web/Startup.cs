using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Core.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Core.Models;
using Core.Repositories;
using Core.Services;

namespace Web
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

            // IoCContaiee
            // TODO: IoC container vullen met nieuwe repo's ook in web
            services.AddScoped<IQuizRepo, QuizRepo>();
            services.AddScoped<IAntwoordRepo, AntwoordRepo>();
            services.AddScoped<IVraagRepo, VraagRepo>();
            services.AddScoped<IQuizGebruikerRepo, QuizGebruikerRepo>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IMoeilijkheidsgraadRepo, MoeilijkheidsgraadRepo>();


            services.AddDbContext<AstrologyQuizDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            services.AddDefaultIdentity<Gebruiker>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AstrologyQuizDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();            
        }
            

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleMgr, UserManager<Gebruiker> userMgr, AstrologyQuizDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Quiz}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            Seeders.SeedRoles(roleMgr).Wait();
            Seeders.SeedUsers(userMgr).Wait();
            
        }
    }
}
