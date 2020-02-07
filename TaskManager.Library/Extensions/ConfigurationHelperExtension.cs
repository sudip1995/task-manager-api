using TaskManager.Library.Helpers;

namespace TaskManager.Library.Extensions
{
    // Extension method in C#
    public static class ConfigurationHelperExtension
    {
        public static string GetDatabaseConnectionString(this IConfigurationHelper source)
        {
            return source["DatabaseSettings:ConnectionString"];
        }
        public static string GetDatabaseName(this IConfigurationHelper source)
        {
            return source["DatabaseSettings:DatabaseName"];
        }
    }
}
