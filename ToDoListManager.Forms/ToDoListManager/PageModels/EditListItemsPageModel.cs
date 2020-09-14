using System.Collections.Generic;
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
            LoadAllListItemsCommand.Execute(false);
        }

        #endregion

        #region Service Mappings

        #endregion

        #region Properties

        #region Data Properties

        public ToDoItem SelectedItem { get; set; }

        public IList<ToDoItem> SelectedListItems { get; set; } = new List<ToDoItem>();

        #endregion

        #region Navigation Properties

        #endregion

        #region State Properties

        public override PageType PageType => PageType.EditListItems;

        #endregion

        #endregion

        #region Commands

        #region Data Commands

        #region LoadAllListItemsCommand

        public Command<bool> LoadAllListItemsCommand =>
            new Command<bool>(async forceLatest =>
                {
                    var selectedListId = NavState.ListSelected?.Id;

                    if (selectedListId == null)
                        return;

                    PageIsWaiting = true;
                    LoadAllListItemsCommand.ChangeCanExecute();

                    SelectedList =
                        await DataService.GetOneList(selectedListId, forceLatest);

                    var selectedListItemIds = NavState.ListSelected?.ItemIds;

                    // TODO Consolidate into GetOneList to avoid multiple API calls in real version
                    if (selectedListItemIds != null)
                        SelectedListItems =
                           await DataService.GetListItems(selectedListItemIds);

                    PageIsWaiting = false;
                    LoadAllListItemsCommand.ChangeCanExecute();
                },
                forceLatest => !PageIsWaiting);

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

        #endregion

        #endregion

        #region Navigation Commands

        #endregion

        #region State Commands

        #endregion

        #endregion

        #region Internal Methods

        #endregion

        #region Protected Methods

        #endregion

        #region Private Methods

        #endregion
    }
}