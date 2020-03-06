using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ODataModel;
using System;

namespace ODataExpandIssue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ParentMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            using (var connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<TestModelContext>()
                    .UseSqlite(connection) // Set the connection explicitly, so it won't be closed automatically by EF
                    .Options;

                // Create the dabase schema
                // You can use MigrateAsync if you use Migrations
                using (var context = new TestModelContext(options))
                {
                    context.Database.EnsureCreated();
                    LoadMockData(context);

                } // The connection is not closed, so the database still exists

                using (var context = new TestModelContext(options))
                {
                    var repository = new ParentRespository(context, mapper);

                    var users = repository.GetAllParents();

                }
            }
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
