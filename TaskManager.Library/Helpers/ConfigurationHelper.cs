using Microsoft.Extensions.Configuration;

namespace TaskManager.Library.Helpers
{
    public class ConfigurationHelper: IConfigurationHelper
    {
        private static IConfigurationHelper _instance;
        private static readonly object LockObject = new object();
        private IConfiguration _configuration;
        public bool IsDevelopment { get; set; }

        public static IConfigurationHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigurationHelper();
                        }
                    }
                }

                return _instance;
            }
        }

        public void AttachConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T GetConfig<T>(string key)
        {
            return (T) _configuration.GetSection(key).Get<T>();
        }

        // Indexer in C#
        public string this[string key] => GetConfig<string>(key);
    }
}