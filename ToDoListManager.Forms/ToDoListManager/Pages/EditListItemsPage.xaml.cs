using System;
using ToDoListManager.PageModels;
using Xamarin.Forms.Xaml;
using static ToDoListManager.App;

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
                MessagingService.SendErrorMessage(exception);
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