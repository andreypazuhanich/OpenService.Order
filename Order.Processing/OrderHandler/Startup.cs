using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Order.Hangfire;
using OrderHandler;

namespace Order
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(s => s.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppDbContext>(s =>
                s.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient <OrderWorker>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IErrorMessagePublisher, ErrorMessagePublisher>();
            services.AddTransient<ILogicCreator, LogicCreator>();
            services.AddSingleton<IBus>(RabbitHutch.CreateBus(Configuration["MQ:Dev"]));
            services.AddControllers();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireDashboard("/HangfireDashboard", new DashboardOptions());
            app.UseHangfireServer(new BackgroundJobServerOptions());
            RecurringJob.AddOrUpdate<OrderWorker>(
            "transferWorkerId",
             (x) => x.Run(),
              "*/5 * * * * *",
               TimeZoneInfo.Local);
            

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}