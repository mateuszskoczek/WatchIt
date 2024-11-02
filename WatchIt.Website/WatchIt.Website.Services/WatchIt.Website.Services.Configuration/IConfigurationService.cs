using WatchIt.Website.Services.Configuration.Model;

namespace WatchIt.Website.Services.Configuration;

public interface IConfigurationService
{
    ConfigurationData Data { get; }
}