using Challenge.RealEstates.Api.Extensions;
using Challenge.RealEstates.Domain.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Challenge.RealEstates.Api
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
            services.AddLogging();
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Challenge.RealEstates.Api",
                    Description = "Swagger surface",
                    Contact = new OpenApiContact()
                    {
                        Name = "Guilherme Saulo Alves",
                        Email = "guisaulo@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/guisaulo/")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/guisaulo/eng-zap-challenge-dotNet/blob/main/LICENSE")
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            services.AddApplicationServices();
            services.ConfigureAutoMapper();
            services.AddGateways();
            services.AddServices();
            services.AddValidatorsFromAssemblyContaining<RealEstateValidator>();
            services.AddRepositories();
            services.AddDataInMemory();
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
            app.UseAuthorization();
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Challenge.RealEstates.Api v1");
                c.OAuthClientId("Swagger");
                c.OAuthClientSecret("swagger");
                c.OAuthAppName("Challenge.RealEstates.Api v1");
                c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
