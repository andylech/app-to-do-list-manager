using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListManager.Data.Responses;

namespace ToDoListManager.Data
{
    // NOTE This is obviously not a real Refit REST interface.  More for identifying requests.
    public interface IToDoListApi
    {
        // CRUD operations for individual lists

        Task<ToDoList> GetOneList(string listId, bool forceLatest = false);

        Task<DateTimeOffset?> SaveList(ToDoList list);

        Task<DateTimeOffset?> DeleteList(ToDoList list);

        // CRUD operations for individual items in a list

        Task<DateTimeOffset?> SaveItem(ToDoItem item, string listId);

        Task<DateTimeOffset?> DeleteItem(ToDoItem item, string listId);

        // CRUD operations for all lists

        Task<IEnumerable<string>> GetAllListIds(bool forceLatest = false);
    }
}
