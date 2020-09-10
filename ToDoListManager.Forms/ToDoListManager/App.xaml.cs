using System;
using System.Reflection;
using FancyLogger;
using ToDoListManager.Models;
using ToDoListManager.Navigation;
using ToDoListManager.PageModels;
using ToDoListManager.Services;
using ToDoListManager.Services.Api;
using ToDoListManager.Services.Messaging;
using Xamarin.Forms;

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
                ServiceManager = new ServiceManager();

                InitializeComponent();

                NavService = new NavigationService(Assembly);

                MainPage = new MainPage();

                MainPage = new NavigationPage();
                // TODO Move initiation into NavService method
                NavService.PushAsync(typeof(EditListItemsPageModel),
                    new NavigationState(AppSection.ListManagement));

            }
            catch (Exception exception)
            {
                MessagingService?.SendErrorMessage(exception);
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

        internal static FancyLoggerService LoggingService =>
            ServiceManager.LoggingService;

        internal static MessagingService MessagingService =>
            ServiceManager.MessagingService;

        internal static ToDoListApiService ToDoListApiService =>
            ServiceManager.ToDoListApiService;

        #endregion

        #region Properties

        #endregion

        #region Private

        #endregion
    }
}
