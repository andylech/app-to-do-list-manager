using System.Diagnostics.CodeAnalysis;
using Akavache.Sqlite3;

// Handling Xamarin Linker
// https://github.com/reactiveui/Akavache
// Add the following class anywhere in your project to make sure
// Akavache.Sqlite3 will not be linked out by Xamarin
namespace ToDoListManager.Services.Caching
{
    public static class LinkerPreserve
    {
        [SuppressMessage("ReSharper", "UnusedVariable")]
        static LinkerPreserve()
        {
            var persistentName = typeof(SQLitePersistentBlobCache).FullName;
            var encryptedName = typeof(SQLiteEncryptedBlobCache).FullName;
        }
    }
}
