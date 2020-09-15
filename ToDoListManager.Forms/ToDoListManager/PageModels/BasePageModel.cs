using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using PropertyChanged;
using ToDoListManager.Data.Responses;
using ToDoListManager.Models;
using ToDoListManager.Pages;
using Xamarin.Forms;
using static ToDoListManager.App;
using static ToDoListManager.Models.AppSection;
using static ToDoListManager.Services.Messaging.MessagingService;

namespace ToDoListManager.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BasePageModel
    {
        #region Fields

        #endregion

        #region Constructors

        protected BasePageModel(NavigationState navState)
        {
            NavState = navState;

            PageModelCreatedCommand.Execute(null);
        }

        #endregion

        #region Service Mappings

        #endregion

        #region Properties

        #region Data Properties

        // Either the open list on EditListItemsPage or the selected list on EditListsPage
        public ToDoList SelectedList { get; set; }

        public string SelectedListId { get; set; }

        public string SelectedListName =>
            !string.IsNullOrWhiteSpace(SelectedList?.Name)
                ? SelectedList.Name
                : !string.IsNullOrWhiteSpace(NavState?.SelectedListName)
                    ? NavState.SelectedListName
                    : "New list";

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

        #region AddNewListCommand

        public Command<string> AddNewListCommand =>
            new Command<string>(async listName =>
                {
                    PageIsWaiting = true;
                    AddNewListCommand.ChangeCanExecute();

                    SelectedList = new ToDoList(listName);
                    await DataService.AddNewList(SelectedList);

                    SelectedListId = SelectedList.Id;
                    await DataService.SaveSelectedListId(SelectedListId);

                    PageIsWaiting = false;
                    AddNewListCommand.ChangeCanExecute();
                },
                listName => !PageIsWaiting);

        #endregion


        #endregion

        #region Navigation Commands

        // TODO Implement GoToAppSectionCommand when add primary navigation

        #region GoToPageCommand

        //// Generic navigation command
        //public Command<PageType> GoToPageCommand =>
        //    new Command<PageType>(async nextPage =>
        //    {
        //        try
        //        {
        //            // TODO Update after primary navigation added
        //            var navState = new NavigationState(ListManagement);

        //            await ExecuteGoToPageCommand(nextPage, navState);
        //        }
        //        catch (Exception exception)
        //        {
        //            SendErrorMessage(exception);
        //        }
        //    });

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
                        // TEMP While only 2 pages and start on EditListItems, can simplify
                        await NavService.PopAsync();
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
                SendErrorMessage(exception);
            }
        }

        #endregion

        #region GoToEditListItemsPageCommand

        public Command GoToEditListItemsPageCommand =>
            new Command(async list =>
            {
                // TODO Send list header
                var navState = new NavigationState(ListManagement);

                await ExecuteGoToPageCommand(PageType.EditListItems, navState);
            });

        #endregion

        #region GoToEditListsPageCommand

        public Command GoToEditListsPageCommand =>
            new Command(async () =>
            {
                var navState = new NavigationState(ListManagement);

                await ExecuteGoToPageCommand(PageType.EditLists, navState);
            });

        #endregion

        #endregion

        #region State Commands

        private Command PageModelCreatedCommand =>
            new Command(() => LoggingService.WriteHeader($"{PageName} - PageModel Created"));

        public Command PageOnAppearingCommand =>
            new Command(() => LoggingService.WriteHeader($"{PageName} - Page OnAppearing"));

        #endregion

        #endregion

        #region Methods

        #region Internal Methods

        internal bool IsExistingList() => !string.IsNullOrWhiteSpace(SelectedListId);

        #endregion

        #region Protected Methods

        #endregion

        #region Private Methods

        #endregion

        #endregion
    }
}