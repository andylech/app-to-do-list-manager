namespace ToDoListManager.Pages
{
    // Divide pages into intro, primary, and secondary allows for quick calculations
    // For example:
    // >= Home && <= Menu => Top-level page => Show primary menu, header/footer, etc.
    // > Menu => Don't show primary menu, header/footer, etc.

    public enum PageType
    {
        EditListItems = 100,
        EditLists = 200
    }
}