using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SalesTransactionService;
using SalesTransactionService.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesTransactionService.Invoice;
using SalesTransactionService.SalesTransaction;

namespace SalesTransactionWebAPI
{
    public class Startup
    {
        public const string CorsPolicy = "AllowCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                   b => b.AllowAnyOrigin() 
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   );
            });

            services.AddScoped<IDatabaseService, DatabaseService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<ICustomerService, CustomerService>()
                .AddScoped<IInvoiceService, InvoiceService>()
                .AddScoped<ISalesTransactionService, SalesTransactionService.SalesTransaction.SalesTransactionService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(CorsPolicy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
