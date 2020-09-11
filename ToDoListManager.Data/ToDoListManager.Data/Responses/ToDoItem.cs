using System;

namespace ToDoListManager.Data.Responses
{
    public class ToDoItem
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public bool Completed { get; set; }

        public bool Deleted { get; set; }

        public string ListId { get; set; }

        public DateTimeOffset SavedTimestamp { get; set; }
    }
}