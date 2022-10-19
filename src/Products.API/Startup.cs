using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Net.Http.Headers;
using Products.API.Clients;
using Products.API.Middleware;
using Products.API.Services;
using Polly;
using Serilog;

namespace Products.API
{
    public class Startup
    {
        private ConfigurationManager _config;
        private IWebHostEnvironment _environment;
        private WebApplicationBuilder _builder;

        public Startup(ConfigurationManager config, IWebHostEnvironment environment, WebApplicationBuilder builder)
        {
            _config = config;
            _environment = environment;
            _builder = builder;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureLogging();
 
            services.AddCors();
 
            // Add services to the container.
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddHttpClient<IProductClient, ProductClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(_config["ProductsProvider:Url"]);
                httpClient.DefaultRequestHeaders.Add("api-key", _config["ProductsProvider:ApiKey"]);
            })
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(1500)))
            .AddTransientHttpErrorPolicy(p => p.RetryAsync(3));

            services.AddSingleton<IProductService, ProductService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Configure the HTTP request pipeline.
            if (_environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureLogging()
        {
            var logger = new LoggerConfiguration()
                          .ReadFrom.Configuration(_config)
                          .Enrich.FromLogContext()
                          .CreateLogger();
            _builder.Logging.ClearProviders();
            _builder.Logging.AddSerilog(logger);
        }
    }
}
