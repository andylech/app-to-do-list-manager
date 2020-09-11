using System;
using System.Runtime.CompilerServices;

// TODO using Refit;

namespace ToDoListManager.Data
{
    public class ToDoListApi
    {
        #region Fields

        // TODO private string baseUrl = "";
        // TODO private readonly IToDoListApi _api;

        #endregion

        #region Constructors

        public ToDoListApi()
        {
            try
            {
                // TODO Add HTTP logger

                // TODO _api = RestService.For<IToDoListApi>(baseUrl);
            }
            catch (Exception exception)
            {
                SaveExceptionLocation(exception);

                throw;
            }
        }

        #endregion

        #region Search

        // TODO

        #endregion

        #region CRUD - Individual Items

        // TODO

        #endregion

        #region CRUD - Individual Lists

        // TODO

        #endregion

        #region CRUD - Multiple Lists

        // TODO

        #endregion

        #region Private

        private void SaveExceptionLocation(Exception exception,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            exception.Data.Add("Member Name", memberName);
            exception.Data.Add("Source File Path", sourceFilePath);
            exception.Data.Add("Source Line Number", sourceLineNumber);
        }

        #endregion
    }
}
