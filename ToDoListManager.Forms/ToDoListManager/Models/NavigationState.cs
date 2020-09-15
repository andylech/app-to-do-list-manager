using System;
using ToDoListManager.Data.Responses;
using static ToDoListManager.App;
using static ToDoListManager.Services.Messaging.MessagingService;

namespace ToDoListManager.Models
{
    public class NavigationState
    {
        #region Constructors

        public NavigationState(AppSection appSection,
            ValueTuple<string, string>? listHeader = null)
        {
            try
            {
                SelectedAppSection = appSection;

                if (listHeader == null)
                    return;

                SelectedListId = listHeader.Value.Item1;
                SelectedListName = listHeader.Value.Item2;
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);
            }
        }

        #endregion

        #region Public

        // Primary navigation (nav bar)
        public AppSection SelectedAppSection { get; }

        // Identifier for currently selected list (if any)
        public string SelectedListId { get; }

        // User-visible name for currently selected list (if any)
        public string SelectedListName { get; }

        #endregion
    }
}