﻿using ToDoListManager.Models;
using ToDoListManager.Pages;

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

        #endregion

        #region Navigation Properties

        #endregion

        #region State Properties

        public override PageType PageType => PageType.EditListItems;

        #endregion

        #endregion

        #region Commands

        #region Data Commands

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