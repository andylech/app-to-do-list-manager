using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDoListManager.Data;
using ToDoListManager.Data.Responses;
using ToDoListManager.Services.Caching;
using ToDoListManager.Services.Resources;
using static ToDoListManager.Services.ServiceManager;
using static ToDoListManager.Services.Messaging.MessagingService;


namespace ToDoListManager.Services.Api
{
    public class ToDoListDataService
    {
        #region Fields

        private readonly CachingService _cachingService;

        private readonly ToDoListRestService _restService;

        #endregion

        #region Constructors

        public ToDoListDataService(string cacheName)
        {
            try
            {
                _cachingService = new CachingService(cacheName);
                _restService = new ToDoListRestService();

                LoggingService?.WriteHeader("Data Service Started");
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);
            }
        }

        #endregion

        #region CRUD - Individual Lists

        // TODO

        #endregion

        #region CRUD - Individual Items

        //public async Task

        // TODO

        #endregion

        #region CRUD - Multiple Lists


        public async Task<IList<ValueTuple<string, string>>> GetAllListHeaders()
        {
            try
            {
                var listHeaders =
                    await CachingService.GetObject<IList<ValueTuple<string, string>>>(
                    CacheKeys.AllListIds,
                    Location.LocalMachine);

                PrintAllListHeaders(listHeaders);

                return listHeaders;
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        #endregion

        #region Private

        [Conditional("DEBUG")]
        public void PrintAllListHeaders(IList<ValueTuple<string, string>> listHeaders)
        {
            if (listHeaders == null)
                return;

            var dictionary = new Dictionary<string, string>(
                listHeaders.ToDictionary(
                    list => list.Item1,
                    list => list.Item2));

            LoggingService?.WriteDictionary(dictionary, "List Ids");
        }

        #endregion
    }
}