using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Data;
using Core.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using System.IO;
using Core.Services;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace API
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
            services.AddControllers();

            //services.AddMvc(options =>
            //{
            //    options.EnableEndpointRouting = false;
            //    options.RespectBrowserAcceptHeader = true;
            //}).AddNewtonsoftJson(options =>
            //{
            //    //circulaire referenties verhinderen door navigatie props
            //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});

            services.AddDbContext<AstrologyQuizDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<Gebruiker>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AstrologyQuizDbContext>();

            //    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //        .AddCookie(
            //       options =>
            //       {
            //           options.Cookie.SameSite = SameSiteMode.None;
            //           options.Events =
            //             new CookieAuthenticationEvents()
            //             {
            //                 OnRedirectToLogin = (ctx) =>
            //                 {
            //                     if (ctx.Request.Path.StartsWithSegments("/api") &&
            //                ctx.Response.StatusCode == 200) //redirect naar loginURL is 200
            //                     {
            //                         //doe geen redirect naar een loginpagina bij een api call 
            //                         //maar geef een 401 (unauthorized) als authenticatie faalt 
            //                         ctx.Response.StatusCode = 401;
            //                         ctx.Response.WriteAsync("{\"error\": " + ctx.Response.StatusCode + " Geen toegang}");
            //                     }

            //                     return Task.CompletedTask;
            //                 }
            //             };
            //       }
            //);

            // TODO: IoC container vullen met nieuwe repo's
            services.AddScoped<IQuizRepo, QuizRepo>();
            services.AddScoped<IAntwoordRepo, AntwoordRepo>();
            services.AddScoped<IVraagRepo, VraagRepo>();
            services.AddScoped<IAuthentiecatieService, AuthentiecatieService>();

            // TODO: Automapper implementer (2 dependencies)
            services.AddAutoMapper(typeof(Startup));
            services.AddCors();

            

             services.AddSwaggerGen(
             c =>
             {
                 c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "AstrologyQuizDB", Version = "v1.0" });
                 // Set the comments path for the Swagger JSON and UI. nodig voor xml te lezen
                 var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                 var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                 c.IncludeXmlComments(xmlPath);

                 // sets authorization
                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     Name = "Authorization",
                     Type = SecuritySchemeType.ApiKey,
                     Scheme = "Bearer",
                     BearerFormat = "JWT",
                     In = ParameterLocation.Header,
                     Description = "JWT Authorization header using the Bearer scheme. Don't forgot Bearer {{bearertoken}}"
                 });

                 c.AddSecurityRequirement(new OpenApiSecurityRequirement
              {
                  {
                        new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                          new string[] { "Bearer"}

                  }
              });
             });

            //if (!env.IsDevelopment())
            //{
            //    services.AddHttpsRedirection(options =>
            //    {
            //        //default: 307 redirect
            //        // options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
            //        options.HttpsPort = 443;
            //    });

            //    services.AddHsts(options =>
            //    {
            //        options.MaxAge = TimeSpan.FromDays(40); //default 30
            //    });
            //}

            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters() {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],

                        IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                };
                options.SaveToken = false;
                options.RequireHttpsMetadata = false;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => {
                builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });

            app.UseAuthentication();
            app.UseAuthorization();

           
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger"; //path naar de UI pagina: /swagger/index.html 
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "AstrologyQuizDB v1.0");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });


        }
    }
}
