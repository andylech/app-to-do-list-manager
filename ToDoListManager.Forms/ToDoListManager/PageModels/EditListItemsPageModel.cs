using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ToDoListManager.Data.Responses;
using ToDoListManager.Models;
using ToDoListManager.Pages;
using Xamarin.Forms;
using static ToDoListManager.App;

namespace ToDoListManager.PageModels
{
    public class EditListItemsPageModel : BasePageModel
    {
        #region Fields

        #endregion

        #region Constructors

        public EditListItemsPageModel(NavigationState navState) : base(navState)
        {
        }

        #endregion

        #region Service Mappings

        #endregion

        #region Properties

        #region Data Properties

        public ToDoItem SelectedItem { get; set; }

        public ObservableCollection<ToDoItem> SelectedListItems { get; set; } =
            new ObservableCollection<ToDoItem>();

        #endregion

        #region Navigation Properties

        #endregion

        #region State Properties

        public override PageType PageType => PageType.EditListItems;

        #endregion

        #endregion

        #region Commands

        #region Data Commands

        #region AddNewItemCommand

        public Command AddNewItemCommand =>
            new Command(async () =>
                {
                    PageIsWaiting = true;
                    AddNewItemCommand.ChangeCanExecute();

                    // NOTE SelectedItem and SelectedList must be updated first
                    await DataService.AddNewItem(SelectedItem.Text, SelectedList.Id);

                    PageIsWaiting = false;
                    AddNewItemCommand.ChangeCanExecute();
                },
                () => !PageIsWaiting);

        #endregion

        #region SaveItemCommand

        public Command SaveItemCommand =>
            new Command(async () =>
                {
                    PageIsWaiting = true;
                    SaveItemCommand.ChangeCanExecute();

                    // NOTE SelectedItem must be updated first
                    await DataService.SaveItem(SelectedItem);

                    PageIsWaiting = false;
                    SaveItemCommand.ChangeCanExecute();
                },
                () => !PageIsWaiting);

        #endregion

        #region CompleteItemCommand

        public Command<bool> CompleteItemCommand =>
            new Command<bool>(async completed =>
                {
                    PageIsWaiting = true;
                    CompleteItemCommand.ChangeCanExecute();

                    // NOTE SelectedItem must be updated first
                    await DataService.CompleteItem(SelectedItem, completed);

                    PageIsWaiting = false;
                    CompleteItemCommand.ChangeCanExecute();
                },
                completed  => !PageIsWaiting);

        #endregion

        #region DeleteItemCommand

        public Command DeleteItemCommand =>
            new Command(async () =>
                {
                    PageIsWaiting = true;
                    DeleteItemCommand.ChangeCanExecute();

                    await DataService.DeleteItem(SelectedItem, SelectedList.Id);

                    PageIsWaiting = false;
                    DeleteItemCommand.ChangeCanExecute();
                },
                () => !PageIsWaiting);

        #endregion

        // DEV only - Use with caution!
        #region ClearListItemsCacheCommand

        public Command ClearListItemsCacheCommand =>
            new Command(() =>
                {
                    PageIsWaiting = true;
                    ClearListItemsCacheCommand.ChangeCanExecute();

                    DataService.ClearListItems(SelectedList.ItemIds);

                    SelectedListItems.Clear();

                    PageIsWaiting = false;
                    ClearListItemsCacheCommand.ChangeCanExecute();
                },
                () => !PageIsWaiting);

        #endregion

        // DEV only - Use with caution!
        #region PopulateListItemsCacheCommand

        public Command PopulateListItemsCacheCommand =>
            new Command(async () =>
                {
                    PageIsWaiting = true;
                    PopulateListItemsCacheCommand.ChangeCanExecute();

                    DataService.PopulateTestListItems();

                    var selectedList = await DataService.GetOneList(SelectedListId);
                    SelectedListItems =
                        new ObservableCollection<ToDoItem>(
                            await DataService.GetListItems(selectedList.ItemIds));

                    PageIsWaiting = false;
                    PopulateListItemsCacheCommand.ChangeCanExecute();
                },
                () => !PageIsWaiting);

        #endregion

        #endregion

        #region Navigation Commands

        #endregion

        #region State Commands

        #endregion

        #endregion

        #region Internal Methods

        // TODO Rewrite later to load list items along with list

        internal async Task LoadSelectedList()
        {
            if (PageIsWaiting || SelectedList != null)
                return;

            // Mimic Command gating to use in sequence with UI prompt if no list available
            PageIsWaiting = true;

            var selectedListId = !string.IsNullOrWhiteSpace(NavState?.SelectedListId)
                ? NavState.SelectedListId
                : await DataService.GetSelectedListId();

            if (string.IsNullOrWhiteSpace(selectedListId))
            {
                PageIsWaiting = false;

                return;
            }

            SelectedList = await DataService.GetOneList(selectedListId, true);

            // Something happened to the list elsewhere
            if (SelectedList == null)
            {
                SelectedListId = null;

                PageIsWaiting = false;

                return;
            }

            SelectedListId = selectedListId;
            await DataService.SaveSelectedListId(SelectedListId);

            SelectedListItems =
                new ObservableCollection<ToDoItem>(
                    await DataService.GetListItems(SelectedList.ItemIds));

            PageIsWaiting = false;
        }

        #endregion

        #region Protected Methods

        #endregion

        #region Private Methods

        #endregion
    }
}