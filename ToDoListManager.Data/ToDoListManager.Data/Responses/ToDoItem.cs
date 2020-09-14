using System;
using System.Diagnostics.CodeAnalysis;

namespace ToDoListManager.Data.Responses
{
    public class ToDoItem
    {
        #region Constructors

        // For Json.Net Serialization/Deserialization
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public ToDoItem()
        {

        }

        public ToDoItem(string text, string listId)
        {
            Text = text;
            Id = Guid.NewGuid().ToString();
            ListId = listId;
        }
        #endregion

        #region Properties

        public string Id { get; }

        public string Text { get; set; }

        public bool Completed { get; set; }

        public bool Deleted { get; set; }

        public string ListId { get; set; }

        public DateTimeOffset ChangedTimestamp { get; set; }

        #endregion
    }
}