using System.Collections;
using System.Collections.Generic;
using ToDoListManager.Data.Responses;

namespace ToDoListManager.Data
{
    // NOTE This is not a real Refit REST interface.  More for identifying types of requests.
    public interface IToDoListApi
    {
        // Search for individual items or lists

        bool CheckListExists(string listId, string listName);

        int? FindItemIndexInList(string itemId, string listId);

        // CRUD operations for all lists

        IEnumerable<ToDoList> GetAllLists();

        // CRUD operations for individual lists

        ToDoList GetOneList();

        void InsertList(ToDoList list);

        // The to-do list's ItemIds contain only the current collection of to-do item Ids
        void UpdateList(ToDoList list);

        void DeleteList(ToDoList list);

        // CRUD operations for individual items in a list

        // Instead of updating old items, they are copied and insert with new Ids
        void InsertItem(ToDoItem item, string listId);

        void DeleteItem(ToDoItem item, string listId);
    }
}
