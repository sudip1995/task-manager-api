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
        public static string GetMinIOEndpoint(this IConfigurationHelper source)
        {
            return source["MinIOS3Settings:Endpoint"];
        }
        public static string GetMinIOAccessKey(this IConfigurationHelper source)
        {
            return source["MinIOS3Settings:AccessKey"];
        }
        public static string GetMinIOSecretKey(this IConfigurationHelper source)
        {
            return source["MinIOS3Settings:SecretKey"];
        }
    }
}
