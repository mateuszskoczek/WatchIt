using Microsoft.Extensions.Configuration;
using WatchIt.Website.Services.Configuration.Model;

namespace WatchIt.Website.Services.Configuration;

public class ConfigurationService(IConfiguration configuration) : IConfigurationService
{
    #region PROPERTIES

    public ConfigurationData Data => configuration.Get<ConfigurationData>()!;

    #endregion
}