using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.UI.Xaml.Controls;
using TestApp01.Interfaces;
using TestApp01.Services;
using TestApp01.ViewModels;
using TestApp01.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestApp01
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// This DI container manages the project's dependencies
        /// Notice: it has a private set and public get
        /// </summary>
        public IServiceProvider Container { get; private set; }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            // bootstrap the whole setup here
            Container = RegisterServices();
            
            m_window = new MainWindow();
            m_window.Activate();
        }

        public static Window m_window;

        /// <summary>
        /// Initializes the DI container
        /// </summary>
        /// <returns></returns>
        private IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();

            var navigationService = new NavigationService();
            navigationService.Configure(nameof(TestPage1), typeof(TestPage1));
            navigationService.Configure(nameof(ItemDetailsPage), typeof(ItemDetailsPage));

            services.AddSingleton<INavigationService>(navigationService);
            services.AddSingleton<IDataService, DataService>();

            // transient means per instance (new object will be returned everytime when asked)
            services.AddTransient<MainViewModel>();
            services.AddTransient<ItemDetailsViewModel>();
            return services.BuildServiceProvider();
        }
    }
}
