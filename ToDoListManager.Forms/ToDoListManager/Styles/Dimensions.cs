using Xamarin.Forms;

namespace ToDoListManager.Styles
{
    // TODO Move this to common XAML styles unless calculated
    public static class Dimensions
    {
        #region Common page dimensions

        private const double HeaderHeight = 60;

        public static GridLength PageHeaderRowHeight = new GridLength(HeaderHeight);

        public static GridLength PageHeaderCenterColumnWidth =
            new GridLength(40, GridUnitType.Star);

        public static GridLength PageHeaderSideColumnWidth =
            new GridLength(30, GridUnitType.Star);

        private const int PageSideMargin = 20;

        public static Thickness PageColumnPadding =
            new Thickness(PageSideMargin, 0);

        #endregion
    }
}
