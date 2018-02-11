using AutoMapper;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using PaceTime.API.Helpers;
using PaceTime.API.Models;
using PaceTime.Data.Core;
using PaceTime.Data.Core.Repositories;
using PaceTime.Data.Security;
using PaceTime.Domain.Interfaces;
using PaceTime.Domain.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace PaceTime.API
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.ReturnHttpNotAcceptable = true;
            })
            .AddRazorPagesOptions(opt =>
            {
                opt.RootDirectory = "/Pages";
            });

            services.AddDbContext<KnowledgeContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:KnowledgeDb"]));

            services.AddScoped<ILibraryRepository, LibraryRepository>();

            services.AddDbContext<SecurityDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:SecurityDb"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<SecurityDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication().AddInstagram(opt =>
            {
                opt.ClientId = Configuration["Authentication:Instagram:ClientId"];
                opt.ClientSecret = Configuration["Authentication:Instagram:ClientSecret"];
                opt.AuthorizationEndpoint = "https://api.instagram.com/oauth/authorize/";
                opt.CallbackPath = "/signin-instagram";
                opt.TokenEndpoint = "https://api.instagram.com/oauth/access_token";
                opt.Scope.Add("basic");
                opt.ClaimsIssuer = "Instagram";
                opt.SaveTokens = true;
                opt.UserInformationEndpoint = "https://api.instagram.com/v1/users/self";
                opt.SignInScheme = IdentityConstants.ExternalScheme;

                opt.Events = new OAuthEvents
                {
                    OnCreatingTicket = async ctx =>
                    {
                        var uri = $"{ctx.Options.UserInformationEndpoint}?access_token={ctx.AccessToken}";
                        var request = new HttpRequestMessage(HttpMethod.Get, uri);

                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var response = await ctx.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ctx.HttpContext.RequestAborted);

                        var data = JObject.Parse(await response.Content.ReadAsStringAsync());
                        var user = data["data"];

                        var userId = user.Value<string>("id");
                        if (!string.IsNullOrEmpty(userId))
                            ctx.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId, ClaimValueTypes.String, ctx.Options.ClaimsIssuer));

                        var formattedName = user.Value<string>("full_name");
                        if (!string.IsNullOrEmpty(formattedName))
                            ctx.Identity.AddClaim(new Claim(ClaimTypes.Name, formattedName, ClaimValueTypes.String, ctx.Options.ClaimsIssuer));

                        var email = user.Value<string>("emailAddress");
                        if (!string.IsNullOrEmpty(email))
                            ctx.Identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String,
                                ctx.Options.ClaimsIssuer));

                        var pictureUrl = user.Value<string>("profile_picture");
                        if (!string.IsNullOrEmpty(pictureUrl))
                            ctx.Identity.AddClaim(new Claim("profile-picture", pictureUrl, ClaimValueTypes.String,
                                ctx.Options.ClaimsIssuer));
                    }
                };
            });

            services.AddSwaggerGen(swg =>
            {
                swg.SwaggerDoc("v1", new Info { Title = "PaceTine.API Swagger Document v1", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                              ILoggerFactory loggerFactory, KnowledgeContext context, SecurityDbContext securityContext)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else if (env.IsStaging())
            {
                app.UseExceptionHandler(b =>
                {
                    b.Run(async ctx =>
                    {
                        ctx.Response.StatusCode = 500;
                        await ctx.Response.WriteAsync("An unexpected fault happend. Try again later.");
                    });
                });
            }

            ConfigureAutoMapper();

            app.UseStaticFiles();

            app.UseAuthentication();

            // This method need to be called after the initial migration is added and database updated.
            context.EnsureSeedData();

            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "PaceTime.API Swagger Document Endpoint");
            });

            app.UseMvc();
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>()
                   .ForMember(dest => dest.FullName, opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"))
                   .ForMember(dest => dest.Age, opt => opt.MapFrom(x => x.DateOfBirth.GetCurrentAge()));

                cfg.CreateMap<Book, BookDto>();
            });
        }
    }
}
