using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FancyLogger;
using ToDoListManager.Models;
using ToDoListManager.Navigation;
using ToDoListManager.PageModels;
using ToDoListManager.Services;
using ToDoListManager.Services.Api;
using ToDoListManager.Services.Caching;
using ToDoListManager.Services.Messaging;
using Xamarin.Forms;
using static ToDoListManager.Services.Messaging.MessagingService;

namespace ToDoListManager
{
    public partial class App : Application
    {
        #region Fields

        private static readonly Assembly Assembly = typeof(App).Assembly;

        #endregion

        #region Constructor


        public App()
        {
            try
            {
                ServiceManager = new ServiceManager(ApplicationName);

                InitializeComponent();

                NavService = new NavigationService(Assembly);

                MainPage = new NavigationPage();
                // TODO Move initiation into NavService method
                NavService.PushAsync(typeof(EditListItemsPageModel),
                    new NavigationState(AppSection.ListManagement));
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);
            }
        }

        #endregion

        #region Protected Overrides

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            CachingService.Shutdown();

            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #endregion

        #region Internal Services

        internal static INavigationService NavService { get; private set; }

        private static ServiceManager ServiceManager { get; set; }

        internal static ToDoListApiService ApiService =>
            ServiceManager.ToDoListApiService;

        internal static CachingService CachingService =>
            ServiceManager.CachingService;

        internal static FancyLoggerService LoggingService =>
            ServiceManager.LoggingService;

        #endregion

        #region Properties

        // TODO Use DependencyService to get from current build configuration when add
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static string ApplicationName => "To-Do List Manager";

        #endregion
    }
}
