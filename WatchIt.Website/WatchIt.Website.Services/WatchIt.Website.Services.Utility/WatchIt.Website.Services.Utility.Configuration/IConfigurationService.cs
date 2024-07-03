using WatchIt.Website.Services.Utility.Configuration.Model;

namespace WatchIt.Website.Services.Utility.Configuration;

public interface IConfigurationService
{
    ConfigurationData Data { get; }
}