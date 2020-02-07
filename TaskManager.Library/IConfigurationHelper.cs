using Microsoft.Extensions.Configuration;

namespace TaskManager.Library
{
    public interface IConfigurationHelper
    {
        void AttachConfigurationProvider(IConfiguration configuration);
        bool IsDevelopment { get; set; }
        T GetConfig<T>(string key);
    }
}