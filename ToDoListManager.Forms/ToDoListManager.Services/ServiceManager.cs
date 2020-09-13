using System;
using FancyLogger;
using ToDoListManager.Services.Api;
using ToDoListManager.Services.Caching;
using ToDoListManager.Services.Messaging;

namespace ToDoListManager.Services
{
    public class ServiceManager
    {
        #region Enums

        #endregion

        #region Fields

        #endregion

        #region Constructors

        public ServiceManager(string appName)
        {
            try
            {
                LoggingService = new FancyLoggerService();

                ToDoListDataService = new ToDoListDataService(appName);
            }
            catch (Exception exception)
            {
                LoggingService?.WriteException(exception);
            }
        }

        #endregion

        #region Public

        public static FancyLoggerService LoggingService { get; private set; }

        public static ToDoListDataService ToDoListDataService { get; private set; }

        #endregion

        #region Interface

        #endregion

        #region Protected

        #endregion

        #region Internal

        #endregion

        #region Private

        #endregion

        #region Nested Types

        #endregion
    }
}
