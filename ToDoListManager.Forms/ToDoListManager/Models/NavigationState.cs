using System;
using static ToDoListManager.App;

namespace ToDoListManager.Models
{
    public class NavigationState
    {
        #region Constructors

        public NavigationState(AppSection appSection,
            Guid? listSelected = null)
        {
            try
            {
                AppSectionSelected = appSection;
                ListSelected = listSelected;
            }
            catch (Exception exception)
            {
                MessagingService.SendErrorMessage(exception);
            }
        }

        #endregion

        #region Public

        // Primary navigation (nav bar)
        public AppSection AppSectionSelected { get; }

        // Identifier for currently selected list (if any)
        public Guid? ListSelected { get; }

        #endregion
    }
}