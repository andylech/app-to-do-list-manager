using System;
using ToDoListManager.PageModels;
using Xamarin.Forms.Xaml;
using static ToDoListManager.App;
using static ToDoListManager.Services.Messaging.MessagingService;

namespace ToDoListManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditListsPage :  BasePage<EditListsPageModel>
    {
        #region Constructors

        public EditListsPage()
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

        #endregion

        #region Bindable Properties

        #endregion

        #region Events

        #endregion

        #region Private

        #endregion
    }
}