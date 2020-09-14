using System;
using System.Threading.Tasks;
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

            await PromptForNameOfListIfMissing();
        }

        #endregion

        #region Bindable Properties

        #endregion

        #region Events

        #endregion

        #region Private

        private async Task PromptForNameOfListIfMissing()
        {
            if (PageModel.IsExistingList())
                return;

            await PromptForNameOfNewList();
        }

        #endregion
    }
}