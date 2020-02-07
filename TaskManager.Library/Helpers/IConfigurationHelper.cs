using Microsoft.Extensions.Configuration;

namespace TaskManager.Library.Helpers
{
    public interface IConfigurationHelper
    {
        void AttachConfigurationProvider(IConfiguration configuration);
        bool IsDevelopment { get; set; }
        T GetConfig<T>(string key);
        string this[string key] { get; }
    }
}