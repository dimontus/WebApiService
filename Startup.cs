using System;
using System.Net.Http;
using AuthenticationBase;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using WebApiService.Clients;

namespace WebApiService
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
            services.AddAppAuthentication(Configuration);
            services.AddServiceClients(Configuration);

            services.AddMvc().AddNewtonsoftJson();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddEntityFrameworkNpgsql().AddDbContext<ProductContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Product")));

            var refitSettings = new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
            };

            //var clientHandler = new HttpClientHandler
            //{
            //    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            //};

            //services.TryAddTransient(_=>RestService.For<IImageClient>(new HttpClient(clientHandler)
            //    {
            //        BaseAddress = new Uri("https://host.docker.internal:8011"),
            //    }, refitSettings));

            //services.TryAddTransient(_=>RestService.For<IPriceClient>(new HttpClient(clientHandler)
            //    {
            //        BaseAddress = new Uri("https://host.docker.internal:8001")
            //    }, refitSettings));

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddForwardedHeadersConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", $"WebApiService API");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
