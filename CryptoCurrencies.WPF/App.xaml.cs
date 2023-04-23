using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CryptoCurrencies.CoinGecko.Interfaces;
using CryptoCurrencies.CoinGecko.Services;
using CryptoCurrencies.WPF.Interfaces;
using CryptoCurrencies.WPF.Services;
using CryptoCurrencies.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoCurrencies.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(ConfigureServices());
        }

        private IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ICoinsService, CoinsService>();

            services.AddSingleton<MainViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<DetailViewModel>();

            services.AddSingleton<HttpClient>();
            services.AddSingleton<Func<Type, BaseViewModel>>(s => viewModelType => (BaseViewModel)s.GetRequiredService(viewModelType));

            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            return services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = Ioc.Default.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
