﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.ReDoc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

#if NETCOREAPP3_0
using Microsoft.Extensions.Hosting;
#endif

namespace ConfigFromFile
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
#if NETCOREAPP3_0
            services.AddMvc(options => options.EnableEndpointRouting = false);
#else
            services.AddMvc();
#endif
            services.AddSwaggerGen();

            services.Configure<SwaggerGenOptions>(Configuration.GetSection("SwaggerGen"));

            services.Configure<SwaggerOptions>(Configuration.GetSection("Swagger"));

            services.Configure<SwaggerUIOptions>(c =>
            {
                Configuration.Bind("SwaggerUI", c);

                c.ConfigObject.SupportedSubmitMethods = new SubmitMethod[] { };
                c.ConfigObject.AdditionalItems.Add("swaggerUIFoo", "bar");
            });

            services.Configure<ReDocOptions>(c =>
            {
                Configuration.Bind("ReDoc", c);

                c.ConfigObject.AdditionalItems.Add("redocFoo", "bar");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#if NETCOREAPP3_0
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#endif
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseReDoc();
        }
    }
}