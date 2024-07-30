using WatchIt.Website.Services.Utility.Configuration;
using WatchIt.Website.Services.Utility.Configuration.Model;
using WatchIt.Website.Services.Utility.Tokens;

namespace WatchIt.Website.Services.WebAPI.Common;

public abstract class BaseWebAPIService
{
    #region SERVICES

    protected readonly IConfigurationService _configurationService;
    
    #endregion
    
    
    
    #region FIELDS

    protected Endpoints EndpointsConfiguration => _configurationService.Data.Endpoints;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    protected BaseWebAPIService(IConfigurationService configurationService)
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