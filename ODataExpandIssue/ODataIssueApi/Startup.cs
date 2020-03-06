using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ODataModel;

namespace ODataIssueApi
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
            var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

            var options = new DbContextOptionsBuilder<TestModelContext>()
                .UseSqlite(connection) // Set the connection explicitly, so it won't be closed automatically by EF
                .Options;

            // Create the dabase schema
            // You can use MigrateAsync if you use Migrations
            var context = new TestModelContext(options);
                    context.Database.EnsureCreated();
            LoadMockData(context);
            services.AddSingleton<TestModelContext>(context);

            services.AddControllers();

            services.AddOData();
            services.AddODataQueryFilter(new EnableQueryAttribute() { PageSize = 100 });

            services.AddMvc(m => m.EnableEndpointRouting = false);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc(routeBuilder => 
            {
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Expand().Select().Count().Filter().OrderBy().MaxTop(10);
            });


        }


        private static void LoadMockData(TestModelContext context)
        {
            context.Parent.Add(new ParentModel() { Key = 1, Name = "1" });
            context.Parent.Add(new ParentModel() { Key = 2, Name = "2" });
            context.Parent.Add(new ParentModel() { Key = 3, Name = "3" });
            context.Parent.Add(new ParentModel() { Key = 4, Name = "4" });

            context.Children.Add(new ChildModel() { Key = 1, ParentKey = 1, Display = "1.1" });
            context.Children.Add(new ChildModel() { Key = 2, ParentKey = 1, Display = "2.1" });
            context.Children.Add(new ChildModel() { Key = 3, ParentKey = 2, Display = "3.2" });
            context.Children.Add(new ChildModel() { Key = 4, ParentKey = 4, Display = "4.4" });
            context.Children.Add(new ChildModel() { Key = 5, ParentKey = 3, Display = "5.3" });

            context.SaveChanges();
        }
    }
}
