﻿using System;
using System.Diagnostics.CodeAnalysis;
using ToDoListManager.Navigation;
using ToDoListManager.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using static ToDoListManager.App;
using static ToDoListManager.Services.Messaging.MessagingService;

namespace ToDoListManager.Pages
{
    public class BasePage<TViewModel> : ContentPage, IPageFor<TViewModel>
        where TViewModel : BasePageModel
    {
        #region Fields

        #endregion

        #region Constructors

        [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
        public BasePage()
        {
            try
            {
                // Use custom header instead of default header nav bar
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

                // Move iOS layout under status bar
                if (Device.RuntimePlatform == Device.iOS)
                    On<iOS>().SetUseSafeArea(true);
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);
            }
        }

        #endregion

        #region Public

        #endregion

        #region Interface

        object IPageFor.PageModel
        {
            get => PageModel;
            set => PageModel = (TViewModel) value;
        }

        public TViewModel PageModel { get; set; }

        #endregion

        #region Protected Overrides

        protected override void OnAppearing()
        {
            base.OnAppearing();

            PageModel.PageOnAppearingCommand.Execute(null);
        }

        #endregion

        #region Bindable Properties

        #endregion

        #region Events

        #endregion

        #region Protected

        #endregion

        #region Private

        #endregion
    }
}