using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using JournalSystem.Repositories.Context;
using Microsoft.EntityFrameworkCore;




namespace JournalSystem
{
    class Program
    {


        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting Task Management System...");

            var host = CreateHostBuilder(args).Build();


            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    Console.WriteLine("Applying migrations...");
                    context.Database.Migrate();
                    Console.WriteLine("Migrations applied successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during database migration: {ex.Message}");
                }
            }



        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .ConfigureServices((context, services) =>
          {
              // Database Context
              services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseSqlServer("Data Source=DESKTOP-4F8B9BO\\MSSQLSERVER01;Initial Catalog=JournalDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;"));



          });
    }
}