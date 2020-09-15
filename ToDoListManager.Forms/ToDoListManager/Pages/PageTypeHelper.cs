using System;
using ToDoListManager.Models;
using ToDoListManager.Resources;
using static ToDoListManager.Services.Messaging.MessagingService;

namespace ToDoListManager.Pages
{
    public static class PageTypeHelper
    {
        public static string PageTitle(this PageType pageType, NavigationState navState)
        {
            var pageTitle = "";

            try
            {
                // Override page title from PageType to make user-friendly or appropriate
                switch (pageType)
                {
                    case PageType.EditListItems:
                        pageTitle = navState.SelectedListName
                            // NOTE This should only appear during development
                            ?? Strings.EditListItemsPageTitle;

                        break;
                    case PageType.EditLists:
                        pageTitle = Strings.EditListsPageTitle;

                        break;
                    default:
                        // Use PageType for default page title and navigation testing
                        pageTitle = pageType.ToString();

                        break;
                }
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);
            }

            return pageTitle;
        }
    }
}