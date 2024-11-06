using WatchIt.Website.Services.Configuration;
using WatchIt.Website.Services.Configuration.Model;

namespace WatchIt.Website.Services.Client;

public abstract class BaseClientService
{
    #region SERVICES

    protected readonly IConfigurationService _configurationService;
    
    #endregion
    
    
    
    #region FIELDS

    protected Endpoints EndpointsConfiguration => _configurationService.Data.Endpoints;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    protected BaseClientService(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }
    
    #endregion



    #region PRIVATE METHODS

    protected abstract string GetServiceBase();

    protected string GetUrl(string suffix) => string.Concat(EndpointsConfiguration.Base, GetServiceBase(), suffix);
    
    protected string GetUrl(string suffix, params object[] format) => string.Concat(EndpointsConfiguration.Base, GetServiceBase(), string.Format(suffix, format));
    
    #endregion
}