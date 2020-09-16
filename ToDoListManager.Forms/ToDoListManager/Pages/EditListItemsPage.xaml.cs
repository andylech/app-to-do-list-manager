using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoListManager.Data.Responses;
using ToDoListManager.PageModels;
using ToDoListManager.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ToDoListManager.App;
using static ToDoListManager.Services.Messaging.MessagingService;

namespace ToDoListManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditListItemsPage : BasePage<EditListItemsPageModel>
    {
        #region Constructors

        public EditListItemsPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);
            }
        }

        #endregion

        #region Interface

        #endregion

        #region Protected Overrides

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await LoadSelectedListOrPromptForNameOfNewList();
        }

        #endregion

        #region Bindable Properties

        #endregion

        #region Events

        //private void CheckBox_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

        #endregion

        #region Private

        private async Task LoadSelectedListOrPromptForNameOfNewList()
        {
            await PageModel.LoadSelectedList();

            if (PageModel.IsExistingList())
                return;

            await PromptForNameOfNewList();
        }

        #endregion

        private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ToDoItem selectedToDoItem = e.CurrentSelection.FirstOrDefault() as ToDoItem;

                // TODO Change selected background color in VisualState in Resource Dictionary
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);
            }
        }
    }
}