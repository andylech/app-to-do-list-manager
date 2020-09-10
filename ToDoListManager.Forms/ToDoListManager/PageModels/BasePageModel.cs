using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ToDoListManager.Models;
using ToDoListManager.Pages;
using Xamarin.Forms;
using static ToDoListManager.App;
using static ToDoListManager.Models.AppSection;

namespace ToDoListManager.PageModels
{
    public abstract class BasePageModel
    {
        #region Fields

        #endregion

        #region Constructors

        protected BasePageModel(NavigationState navState)
        {
            NavState = navState;

            LoggingService.WriteHeader(PageName);
        }

        #endregion

        #region Service Mappings

        #endregion

        #region Properties

        #region Data Properties

        #endregion

        #region Navigation Properties

        public NavigationState NavState { get; private set; }

        #endregion

        #region State Properties

        public bool PageIsWaiting { get; set; }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public string PageName => PageType.ToString();

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public string PageTitle => PageType.PageTitle(NavState);

        public abstract PageType PageType { get; }

        #endregion

        #endregion

        #region Commands

        #region Data Commands

        #endregion

        #region Navigation Commands

        // TODO Implement GoToAppSectionCommand when add primary navigation

        #region GoToPageCommand

        // Generic navigation command
        public Command<PageType> GoToPageCommand =>
            new Command<PageType>(async nextPage =>
            {
                try
                {
                    // TODO Update after primary navigation added
                    var navState = new NavigationState(ListManagement);

                    await ExecuteGoToPageCommand(nextPage, navState);
                }
                catch (Exception exception)
                {
                    MessagingService.SendErrorMessage(exception);
                }
            });

        // This is the brains of nav logic.  Shared by generic and page-specific nav commands.
        private async Task ExecuteGoToPageCommand(PageType nextPage,
            NavigationState navState)
        {
            var currentPage = PageType;

            // Already on the page => ignore the request to avoid redraw/reload
            // TODO More useful w/ primary navigation
            if (currentPage == nextPage)
                return;

            try
            {
                LoggingService.WriteSubheader("Navigation initiated");
                LoggingService.WriteValue("Current Page", currentPage);
                LoggingService.WriteValue("Next Page", nextPage);

                switch (nextPage)
                {
                    case PageType.EditListItems:
                        // Make root page of app
                        await NavService.ReplaceRootAsync(typeof(EditListItemsPageModel),
                            navState);
                        break;
                    case PageType.EditLists:
                        // Push onto nav stack as child page
                        await NavService.PushAsync(typeof(EditListsPageModel),
                            navState);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(nextPage), nextPage,
                            null);
                }
            }
            catch (Exception exception)
            {
                MessagingService.SendErrorMessage(exception);
            }
        }

        #endregion

        #region GoToEditListItemsPageCommand

        public Command<NavigationState> GoToEditListItemsPageCommand =>
            new Command<NavigationState>(async navState =>
                await ExecuteGoToPageCommand(PageType.EditListItems, navState));

        #endregion

        #region GoToEditListsPageCommand

        public Command<NavigationState> GoToEditListsPageCommand =>
            new Command<NavigationState>(async navState =>
                await ExecuteGoToPageCommand(PageType.EditLists, navState));

        #endregion

        #endregion

        #region State Commands

        #endregion

        #endregion
    }
}