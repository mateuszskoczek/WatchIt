using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.WebAPI.Services.Utility.Configuration.Models;

namespace WatchIt.WebAPI.Services.Utility.Configuration
{
    public interface IConfigurationService
    {
        ConfigurationData Data { get; }
    }



    public class ConfigurationService(IConfiguration configuration) : IConfigurationService
    {
        #region PROPERTIES

        public ConfigurationData Data => configuration.GetSection("WebAPI").Get<ConfigurationData>()!;

        #endregion
    }
}
