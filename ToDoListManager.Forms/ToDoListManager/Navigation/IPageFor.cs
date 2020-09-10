using ToDoListManager.PageModels;

namespace ToDoListManager.Navigation
{
    // Adapted from "Xamarin Forms – View Model First Navigation":
    // https://codemilltech.com/xamarin-forms-view-model-first-navigation/
    // https://github.com/codemillmatt/XamFormsVMNav

    public interface IPageFor
    {
        object PageModel { get; set; }
    }

    public interface IPageFor<T> : IPageFor where T : BasePageModel
    {
        new T PageModel { get; set; }
    }
}