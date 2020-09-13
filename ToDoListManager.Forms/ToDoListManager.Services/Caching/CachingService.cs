using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using static ToDoListManager.Services.ServiceManager;
using static ToDoListManager.Services.Messaging.MessagingService;

namespace ToDoListManager.Services.Caching
{
    public class CachingService
    {
        #region Constructors

        public CachingService(string cacheName)
        {
            CacheName = cacheName;

            Startup(cacheName);
        }

        #endregion

        #region Properties

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
        public string CacheName { get; }

        #endregion

        #region Administration

        private static void Startup(string cacheName)
        {
            try
            {
                Registrations.Start(cacheName);

                PrintKeys();
            }
            catch (Exception exception)
            {
                exception.Data.Add("cacheName", cacheName);

                SendErrorMessage(exception);

                throw;
            }
        }

        public static void Shutdown()
        {
            try
            {
                Flush();
            }
            catch (Exception exception)
            {
                SendErrorMessage(exception);

                throw;
            }
        }

        #endregion

        #region Get Operations

        // Akavache: Attempt to return an object from the cache. If the item doesn't
        // exist or returns an error, call a Func to return the latest
        // version of an object and insert the result in the cache.
        public static async Task<T> GetOrFetchObject<T>(string key,
            Location location,
            Func<Task<T>> fetchFunc,
            DateTimeOffset? expiration = null)
        {
            try
            {
                switch (location)
                {
                    case Location.LocalMachine:
                        return await BlobCache.LocalMachine
                            .GetOrFetchObject<T>(key, fetchFunc, expiration);
                    case Location.UserAccount:
                        return await BlobCache.UserAccount
                            .GetOrFetchObject<T>(key, fetchFunc, expiration);
                    case Location.Secure:
                        return await BlobCache.Secure
                            .GetOrFetchObject<T>(key, fetchFunc, expiration);
                    case Location.InMemory:
                        return await BlobCache.InMemory
                            .GetOrFetchObject<T>(key, fetchFunc, expiration);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(location), location, null);
                }
            }
            catch (Exception exception)
            {
                exception.Data.Add("key", key);
                exception.Data.Add("location", location.ToString());

                SendErrorMessage(exception);

                throw;
            }
        }

        #endregion

        #region Insert Operations

        // Akavache: Insert a single object
        public static async Task<Unit> InsertObject<T>(string key,
            Location location,
            T value,
            DateTimeOffset? expiration = null)
        {
            try
            {
                switch (location)
                {
                    case Location.LocalMachine:
                        return await BlobCache.LocalMachine.InsertObject(key,
                            value,
                            expiration);
                    case Location.UserAccount:
                        return await BlobCache.UserAccount.InsertObject(key,
                            value,
                            expiration);
                    case Location.Secure:
                        return await BlobCache.Secure.InsertObject(key,
                            value,
                            expiration);
                    case Location.InMemory:
                        return await BlobCache.InMemory.InsertObject(key,
                            value,
                            expiration);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(location), location, null);
                }
            }
            catch (Exception exception)
            {
                exception.Data.Add("key", key);
                exception.Data.Add("location", location.ToString());

                SendErrorMessage(exception);

                throw;
            }
        }

        #endregion

        #region Delete Operations

        // Akavache: Delete a single object
        public static async Task<Unit> InvalidateObject<T>(string key, Location location)
        {
            try
            {
                switch (location)
                {
                    case Location.LocalMachine:
                        return await BlobCache.LocalMachine.InvalidateObject<T>(key);
                    case Location.UserAccount:
                        return await BlobCache.UserAccount.InvalidateObject<T>(key);
                    case Location.Secure:
                        return await BlobCache.Secure.InvalidateObject<T>(key);
                    case Location.InMemory:
                        return await BlobCache.InMemory.InvalidateObject<T>(key);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(location), location, null);
                }
            }
            catch (Exception exception)
            {
                exception.Data.Add("key", key);
                exception.Data.Add("location", location.ToString());

                SendErrorMessage(exception);

                throw;
            }
        }

        // Akavache: Deletes all items (regardless if they are objects or not)
        private static async Task<Unit> InvalidateAll(Location location)
        {
            try
            {
                switch (location)
                {
                    case Location.LocalMachine:
                        return await BlobCache.LocalMachine.InvalidateAll();
                    case Location.UserAccount:
                        return await BlobCache.UserAccount.InvalidateAll();
                    case Location.Secure:
                        return await BlobCache.Secure.InvalidateAll();
                    case Location.InMemory:
                        return await BlobCache.InMemory.InvalidateAll();
                    default:
                        throw new ArgumentOutOfRangeException(nameof(location), location, null);
                }

            }
            catch (Exception exception)
            {
                exception.Data.Add("location", location.ToString());

                SendErrorMessage(exception);

                throw;
            }
        }

        #endregion

        #region Update Operations

        public static async Task<Unit> UpdateObject<T>(string key,
            Location location,
            T value,
            DateTimeOffset? expiration = null)
        {
            var deleteResult = await InvalidateObject<T>(key, location);

            if (deleteResult != Unit.Default)
                return await InsertObject(key, location, value, expiration);

            return default;
        }

        #endregion

        #region Utility Operations

        // Akavache: Attempt to ensure all outstanding operations are written to disk
        private static void Flush()
        {
            BlobCache.LocalMachine.Flush().Wait();
            BlobCache.UserAccount.Flush().Wait();
            BlobCache.Secure.Flush().Wait();
            BlobCache.InMemory.Flush().Wait();
        }

        // Akavache: Preemptively drop all expired keys and run SQLite's VACUUM method on
        // the underlying database
        private static void Vacuum()
        {
            BlobCache.LocalMachine.Vacuum().Wait();
            BlobCache.UserAccount.Vacuum().Wait();
            BlobCache.Secure.Vacuum().Wait();
            BlobCache.InMemory.Vacuum().Wait();
        }

        #endregion

        #region Debugging

        public static void PrintKeys(Location? location = null)
        {
            PrintKeysByCache(location);
        }

        [Conditional("DEBUG")]
        private static void PrintKeysByCache(Location? location)
        {
            if (location == null || location == Location.LocalMachine)
            {
                PrintKeysToDebug(BlobCache.LocalMachine.GetAllKeys().Wait(),
                    "Local Machine");
            }

            if (location == null || location == Location.UserAccount)
            {
                PrintKeysToDebug(BlobCache.UserAccount.GetAllKeys().Wait(),
                    "User Account");
            }

            if (location == null || location == Location.Secure)
            {
                PrintKeysToDebug(BlobCache.Secure.GetAllKeys().Wait(),
                    "Secure");
            }

            if (location == null || location == Location.InMemory)
            {
                PrintKeysToDebug(BlobCache.InMemory.GetAllKeys().Wait(),
                    "In Memory");
            }
        }

        [Conditional("DEBUG")]
        private static void PrintKeysToDebug(IEnumerable<string> keys, string location)
        {
            var keyList = (IList<string>) keys;
            if (keyList.Count > 0)
            {
                Debug.WriteLine($"Cache - {location}:");

                foreach (var key in keys)
                {
                    Debug.WriteLine($"\t{key}");
                }
            }
        }

        #endregion

    }
}