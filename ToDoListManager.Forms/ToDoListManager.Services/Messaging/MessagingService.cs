using System;
using System.Diagnostics;
using static ToDoListManager.Services.ServiceManager;

namespace ToDoListManager.Services.Messaging
{
    public static class MessagingService
    {
        #region Fields

        #endregion

        #region Constructors

        #endregion

        #region Public

        public static void SendErrorMessage(Exception exception)
        {
            try
            {
                // TODO Send error info to analytics service

                LoggingService?.WriteException(exception);
            }
            catch (Exception)
            {
                Debug.WriteLine("Exception = {0}", exception.Message);
            }
        }

        // TODO

        #endregion

        #region Private

        #endregion
    }
}
