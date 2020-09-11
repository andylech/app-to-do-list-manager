namespace ToDoListManager.Services.Caching
{
    public static class LocationHelper
    {
        public static string LocationName(this Location location)
        {
            var locationName = "";

            switch (location)
            {
                case Location.InMemory:
                    locationName = "In Memory";
                    break;
                case Location.LocalMachine:
                    locationName = "Local Machine";
                    break;
                case Location.UserAccount:
                    locationName = "User Account";
                    break;
                default:
                    locationName = nameof(location);
                    break;
            }

            return locationName;
        }
    }
}