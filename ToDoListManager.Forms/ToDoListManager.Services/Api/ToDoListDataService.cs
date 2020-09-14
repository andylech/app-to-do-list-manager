using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ToDoListManager.Data;
using ToDoListManager.Data.Responses;
using ToDoListManager.Services.Caching;
using ToDoListManager.Services.Resources;
using Xamarin.Forms.Internals;
using static ToDoListManager.Services.ServiceManager;
using static ToDoListManager.Services.Messaging.MessagingService;


namespace ToDoListManager.Services.Api
{
    public class ToDoListDataService
    {
        #region Fields

        private const Location CachingLocation = Location.LocalMachine;

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

        public async Task<ToDoList> GetOneList(string listId, bool forceLatest = false)
        {
            try
            {
                // No-API version gets list from cache versus GetOrFetchObject<T>
                var list = await CachingService.GetObject<ToDoList>(listId, CachingLocation);

                PrintOneList(list);

                return list;
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        public async Task<IList<ToDoItem>> GetListItems(IList<string> itemIds)
        {
            try
            {
                var listItems = new List<ToDoItem>();

                foreach (var itemId in itemIds)
                {
                    var listItem = await GetOneItem(itemId, true);

                    if (listItem != null)
                        listItems.Add(listItem);
                }

                // TODO Print list items

                return listItems;
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        public async Task<DateTimeOffset?> AddNewList(string name)
        {
            try
            {
                // TODO Validate list name in UI
                if (string.IsNullOrWhiteSpace(name))
                    throw new System.ArgumentException("Invalid list name '{name}'");

                var list = new ToDoList(name);

                await AddNewListHeader(list);

                return await SaveList(list);
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        public async Task<DateTimeOffset?> SaveList(ToDoList list)
        {
            return await UpdateList(list, "SAVED");
        }

        public async Task<DateTimeOffset?> DeleteList(ToDoList list)
        {
            try
            {
                // No-API version does "soft" delete which updates list with Deleted flag on
                list.Deleted = true;

                await DeleteListHeader(list);

                return await UpdateList(list, "DELETED");
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        private async Task<DateTimeOffset?> UpdateList(ToDoList list, string operation = null)
        {
            try
            {
                // TODO Validate list in UI
                if (list?.Id == null || string.IsNullOrEmpty(list?.Name))
                    throw new System.ArgumentException("Invalid list");

                // No-API version creates a timestamp on client side instead of on server side
                list.ChangedTimestamp = DateTimeOffset.Now;

                // No-API version doesn't use cache expiration since no source to cache from
                await CachingService.UpdateObject(list.Id, CachingLocation, list);

                PrintOneList(list, operation);

                return list.ChangedTimestamp;
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        private async Task<DateTimeOffset?> AddItemToList(string itemId, string listId)
        {
            try
            {
                var list = await GetOneList(listId, true);
                list.ItemIds.Add(itemId);

                return await UpdateList(list, "ITEMS");
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        private async Task<DateTimeOffset?> DeleteItemFromList(string itemId, string listId)
        {
            try
            {
                var list = await GetOneList(listId, true);
                list.ItemIds.Remove(itemId);

                return await UpdateList(list, "ITEMS");
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        #endregion

        #region CRUD - Individual Items


        public async Task<ToDoItem> GetOneItem(string itemId, bool forceLatest = false)
        {
            try
            {
                // No-API version gets item from cache versus GetOrFetchObject<T>
                var item = await CachingService.GetObject<ToDoItem>(itemId, CachingLocation);

                return item;
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        public async Task<DateTimeOffset?> AddNewItem(string text, string listId)
        {
            try
            {
                // TODO Validate item text in UI
                if (string.IsNullOrWhiteSpace(text))
                    throw new System.ArgumentException($"Invalid list item '{text}'");

                var item = new ToDoItem(text, listId);

                await AddItemToList(item.Id, listId);

                return await UpdateItem(item, "NEW");
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        public async Task<DateTimeOffset?> SaveItem(ToDoItem item)
        {
            return await UpdateItem(item, "SAVED");
        }

        public async Task<DateTimeOffset?> CompleteItem(ToDoItem item)
        {
            // No-API version does "soft" complete which updates item with Complete flag on
            item.Completed = true;

            return await UpdateItem(item, "COMPLETED");
        }

        public async Task<DateTimeOffset?> DeleteItem(ToDoItem item, string listId)
        {
            // No-API version does "soft" delete which updates item with Deleted flag on
            item.Deleted = true;

            await DeleteItemFromList(item.Id, listId);

            return await UpdateItem(item, "DELETED");
        }

        private async Task<DateTimeOffset?> UpdateItem(ToDoItem item, string operation = null)
        {
            try
            {
                // TODO Validate item in UI
                if (item?.Id == null || string.IsNullOrEmpty(item?.Text))
                    throw new System.ArgumentException("Invalid item");

                // No-API version creates a timestamp on client side instead of server side
                item.ChangedTimestamp = DateTimeOffset.Now;

                // No-API version doesn't use cache expiration since no source to cache from
                await CachingService.UpdateObject(item.Id, CachingLocation, item);

                PrintOneItem(item, operation);

                return item.ChangedTimestamp;
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        #endregion

        #region CRUD - Multiple Lists

        // TODO Split list header out into separate object so can timestamp it


        public async Task<IList<ValueTuple<string, string>>> GetAllListHeaders(bool forceLatest = false)
        {
            try
            {
                // No-API version gets Ids from cache versus GetOrFetchObject<T>
                var listHeaders =
                    await CachingService.GetObject<IList<ValueTuple<string, string>>>(
                        CacheKeys.AllListHeaders, CachingLocation);

                PrintAllListHeaders(listHeaders);

                return listHeaders;
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        private async Task<DateTimeOffset?> AddNewListHeader(ToDoList list)
        {
            try
            {
                // TODO Validate item in UI
                if (list?.Id == null || string.IsNullOrEmpty(list?.Name))
                    throw new System.ArgumentException("Invalid list");

                var listHeaders = await GetAllListHeaders(true)
                                  ?? new List<ValueTuple<string, string>>();

                var listHeader = (Id: list.Id, Name: list.Name);
                listHeaders.Add(listHeader);

                return await UpdateListHeaders(listHeaders);
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        private async Task<DateTimeOffset?> DeleteListHeader(ToDoList list)
        {
            try
            {
                var listHeaders = await GetAllListHeaders(true)
                                  ?? new List<ValueTuple<string, string>>();

                var listHeader = (Id: list.Id, Name: list.Name);
                listHeaders.Remove(listHeader);

                return await UpdateListHeaders(listHeaders);
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        private async Task<DateTimeOffset?> UpdateListHeaders(
            IList<ValueTuple<string, string>> listHeaders)
        {
            try
            {
                // No-API version doesn't use cache expiration since no source to cache from
                await CachingService.UpdateObject(CacheKeys.AllListHeaders,
                    CachingLocation, listHeaders);

                return DateTimeOffset.Now;
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                return null;
            }
        }

        #endregion

        #region Debug

        [Conditional("DEBUG")]
        public void PrintAllListHeaders(IList<ValueTuple<string, string>> listHeaders)
        {
            if (listHeaders == null)
                return;

            try
            {
                var dictionary = new Dictionary<string, string>(
                    listHeaders.ToDictionary(
                        list => list.Item1,
                        list => list.Item2));

                LoggingService?.WriteDictionary(dictionary, "List Headers");
            }
            catch (Exception exception)
            {
                // DEV Catch duplicate list Ids being added to the dictionary
                SendErrorMessage(exception);
            }
        }

        [Conditional("DEBUG")]
        private void PrintOneList(ToDoList list, string operation = null)
        {
            if (list == null)
                return;

            var listHeader = string.IsNullOrEmpty(operation) ? "" : $"{operation}: ";
            listHeader += $"List '{list.Name}' = {list.Id}";

            LoggingService.WriteValue(listHeader, $"{list.ItemIds.Count()} items");
        }

        [Conditional("DEBUG")]
        private void PrintOneItem(ToDoItem item, string operation = null)
        {
            if (item == null)
                return;

            var itemHeader = string.IsNullOrEmpty(operation) ? "" : $"{operation}: ";
            itemHeader += $"Item '{item.Text}' = {item.Id}";

            LoggingService.WriteValue(itemHeader, $"list {item.ListId}");
        }

        #endregion
    }
}