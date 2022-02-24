using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SBTBackEnd.Data;
using SBTBackEnd.Entities;
using SBTBackEnd.Error;
using SBTBackEnd.Extensions;
using SBTBackEnd.Services.AuthService;
using SBTBackEnd.Services.FeedbackService;
using SBTBackEnd.Services.ProductService;
using SBTBackEnd.Services.TokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTBackEnd
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            _Config = config;
        }

        private readonly IConfiguration _Config;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContextPool<DataContext>(x => { x.UseSqlServer(_Config.GetConnectionString("DefualtConnection")); });
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddIdentityservice(_Config);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationResponse
                    {
                        Errors=errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "";
                options.ClientSecret = "";
            }).AddFacebook(options =>
            {
                options.AppId = "";
                options.AppSecret = "";
            }).AddTwitter(options =>
            {
                options.ConsumerKey = "";
                options.ConsumerSecret = "";
            });



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SBTBackEnd", Version = "v1" });
            });
            services.AddCors(options => 
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });       
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SBTBackEnd v1"));
            }
            else
            {
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SBTBackEnd v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            //app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
