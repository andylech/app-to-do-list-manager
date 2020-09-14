using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ToDoListManager.Data.Responses
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class ToDoList
    {
        #region Constructors

        // For Json.Net Serialization/Deserialization
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public ToDoList()
        {

        }

        public ToDoList(string name)
        {
            Name = name;
            Id = Guid.NewGuid().ToString();
            ItemIds = new List<string>();
        }

        #endregion

        #region Properties

        public string Id { get; }

        public string Name { get; }

        public bool Deleted { get; set; }

        public IList<string> ItemIds { get; }

        public DateTimeOffset ChangedTimestamp { get; set; }

        #endregion
    }
}