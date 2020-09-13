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
                CachingService = new CachingService(appName);

                LoggingService = new FancyLoggerService();


                ToDoListApiService = new ToDoListApiService();
            }
            catch (Exception exception)
            {
                LoggingService?.WriteException(exception);
            }
        }

        #endregion

        #region Public

        public static CachingService CachingService { get; private set;  }

        public static FancyLoggerService LoggingService { get; private set; }


        public static ToDoListApiService ToDoListApiService { get; private set; }

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
