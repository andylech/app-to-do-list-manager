using System;
using System.Collections.Generic;
using ToDoListManager.Data.Responses;

namespace ToDoListManager.Data
{
    // NOTE This is obviously not a real Refit REST interface.  More for identifying requests.
    public interface IToDoListApi
    {
        //// Search for individual items or lists

        //bool CheckListExists(string listId, string listName);

        //int? FindItemIndexInList(string itemId, string listId);

        // CRUD operations for all lists

        IEnumerable<ToDoList> GetAllLists();

        // CRUD operations for individual lists

        ToDoList GetOneList();

        DateTimeOffset SaveList(ToDoList list);

        DateTimeOffset DeleteList(ToDoList list);

        // CRUD operations for individual items in a list

        DateTimeOffset SaveItem(ToDoItem item, string listId);

        DateTimeOffset DeleteItem(ToDoItem item, string listId);
    }
}
