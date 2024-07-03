using WatchIt.Website.Services.Utility.Configuration;
using WatchIt.Website.Services.Utility.Configuration.Model;

namespace WatchIt.Website.Services.WebAPI.Common;

public abstract class BaseWebAPIService(IConfigurationService configurationService)
{
    #region FIELDS

    protected Endpoints EndpointsConfiguration => configurationService.Data.Endpoints;
    
    #endregion



    #region PRIVATE METHODS

    protected abstract string GetServiceBase();

    protected string GetUrl(string suffix) => string.Concat(EndpointsConfiguration.Base, GetServiceBase(), suffix);
    
    protected string GetUrl(string suffix, params object[] format) => string.Concat(EndpointsConfiguration.Base, GetServiceBase(), string.Format(suffix, format));
    
    #endregion
}