using System;

namespace ToDoListManager.Services.Messaging
{
    public class MessagingService
    {
        #region Fields

        #endregion

        #region Constructors

        #endregion

        #region Public

        public void SendErrorMessage(Exception exception)
        {
            // TEMP
            ServiceManager.LoggingService?.WriteException(exception);
        }

        // TODO

        #endregion

        #region Private

        #endregion
    }
}
