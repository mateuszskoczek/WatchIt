using Microsoft.Extensions.Configuration;
using WatchIt.WebAPI.Services.Utility.Configuration.Model;

namespace WatchIt.WebAPI.Services.Utility.Configuration;

public class ConfigurationService(IConfiguration configuration) : IConfigurationService
{
    #region PROPERTIES

    public ConfigurationData Data => configuration.Get<ConfigurationData>()!;

    #endregion
}