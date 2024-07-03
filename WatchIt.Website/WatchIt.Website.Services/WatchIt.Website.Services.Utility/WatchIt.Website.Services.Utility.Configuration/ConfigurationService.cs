using Microsoft.Extensions.Configuration;
using WatchIt.Website.Services.Utility.Configuration.Model;

namespace WatchIt.Website.Services.Utility.Configuration;

public class ConfigurationService(IConfiguration configuration) : IConfigurationService
{
    #region PROPERTIES

    public ConfigurationData Data => configuration.Get<ConfigurationData>()!;

    #endregion
}