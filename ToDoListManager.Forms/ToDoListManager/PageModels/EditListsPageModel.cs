using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListManager.Models;
using ToDoListManager.Pages;
using Xamarin.Forms;
using static ToDoListManager.App;

namespace ToDoListManager.PageModels
{
    public class EditListsPageModel : BasePageModel
    {
        #region Fields

        #endregion

        #region Constructors

        public EditListsPageModel(NavigationState navState) : base(navState)
        {
            LoadAllListIdsCommand.Execute(false);
        }

        #endregion

        #region Service Mappings

        #endregion

        #region Properties

        #region Data Properties

        private IList<ValueTuple<string, string>> ListHeaders { get; set; }

        #endregion

        #region Navigation Properties

        #endregion

        #region State Properties

        public override PageType PageType => PageType.EditLists;

        #endregion

        #endregion

        #region Commands

        #region Data Commands

        #region LoadAllListIdsCommand

        public Command<bool> LoadAllListIdsCommand =>
            new Command<bool>(async forceLatest =>
                {
                    PageIsWaiting = true;
                    LoadAllListIdsCommand.ChangeCanExecute();

                    ListHeaders = await DataService.GetAllListHeaders(forceLatest);

                    PageIsWaiting = false;
                    LoadAllListIdsCommand.ChangeCanExecute();
                },
                forceLatest => !PageIsWaiting);

        #endregion

        #region DeleteListCommand

        public Command DeleteListCommand =>
            new Command(async () =>
                {
                    PageIsWaiting = true;
                    DeleteListCommand.ChangeCanExecute();

                    await DataService.DeleteListById(SelectedList.Id);

                    PageIsWaiting = false;
                    DeleteListCommand.ChangeCanExecute();
                },
                () => !PageIsWaiting);

        #endregion


        #region RenameListCommand

        public Command<string> RenameListCommand =>
            new Command<string>(async listName =>
                {
                    PageIsWaiting = true;
                    RenameListCommand.ChangeCanExecute();

                    SelectedList.Name = listName;

                    await DataService.RenameListById(SelectedList.Id, listName);

                    PageIsWaiting = false;
                    RenameListCommand.ChangeCanExecute();
                },
                listName => !PageIsWaiting);

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