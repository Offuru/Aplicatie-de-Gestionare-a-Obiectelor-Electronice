using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Core;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Database;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Database.Repositories;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;



        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<DatabaseContext>();
            services.AddScoped<DbContext, DatabaseContext>();
            services.AddScoped<ElectronicObjectRepository>();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            using (DbContext dbContext = new DatabaseContext())
            {
                dbContext.Database.Migrate();
            }

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ElectronicListViewModel>();
            services.AddSingleton<FormViewModel>();
            services.AddSingleton<MenuViewModel>();
            services.AddSingleton<ElectronicOverviewViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<ViewModelLocator>();
            services.AddSingleton<WindowMapper>();
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<IItemsService, ItemsService>();

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var windowManager = _serviceProvider.GetRequiredService<IWindowManager>();
            windowManager.ShowWindow(_serviceProvider.GetRequiredService<MainViewModel>());
            base.OnStartup(e);
        }
    }

}
