using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using JournalSystem.Repositories.Context;
using JournalSystem.Services;
using JournalSystem.Core.Services;
using JournalSystem.Core.Repositories;
using JournalSystem.Repositories;

namespace JournalSystemUI
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        private IHost _host;

        protected override async void OnStartup(StartupEventArgs e)
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    string connectionString = "Data Source=DESKTOP-4F8B9BO\\MSSQLSERVER01;Initial Catalog=JournalDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(connectionString));

                    // Register services and repositories
                    services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();
                    services.AddScoped<IJournalEntryService, JournalEntryService>();
                    services.AddScoped<IJournalLineRepository, JournalLineRepository>();
                    services.AddScoped<IJournalLineService, JournalLineService>();
                    services.AddScoped<IAccountRepository, AccountRepository>();
                    services.AddScoped<IAccountService, AccountService>();
                    services.AddScoped<ICostCenterRepository, CostCenterRepository>();
                    services.AddScoped<ICostCenterService, CostCentreService>();
                    services.AddScoped<ICurrencyRepository, CurrencyRepository>();
                    services.AddScoped<ICurrencyService, CurrencyService>();
                    services.AddScoped<IDimensionRepository, DimensionRepository>();
                    services.AddScoped<IDimensionService, DimensionService>();

                    services.AddAutoMapper(typeof(MappingProfile));

                    
                    services.AddSingleton<MainWindow>();
                })
                .Build();

            await _host.StartAsync(); 

            ServiceProvider = _host.Services;
            base.OnStartup(e);

           
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

    }
}

