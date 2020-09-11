using System;
using System.Collections.Generic;

namespace ToDoListManager.Data.Responses
{
    public class ToDoList
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }

        public IEnumerable<string> ItemIds { get; set; }

        public DateTimeOffset SavedTimestamp { get; set; }
    }
}