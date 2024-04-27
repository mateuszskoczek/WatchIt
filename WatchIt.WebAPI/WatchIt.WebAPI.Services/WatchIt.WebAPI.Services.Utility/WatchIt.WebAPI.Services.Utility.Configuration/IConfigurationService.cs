using WatchIt.WebAPI.Services.Utility.Configuration.Model;

namespace WatchIt.WebAPI.Services.Utility.Configuration;

public interface IConfigurationService
{
    ConfigurationData Data { get; }
}