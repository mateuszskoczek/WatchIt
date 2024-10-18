namespace WatchIt.Website.Services.Utility.Configuration.Model;

public class ConfigurationData
{
    public Logging Logging { get; set; }
    public string AllowedHosts { get; set; }
    public StorageKeys StorageKeys { get; set; }
    public Style Style { get; set; }
    public Endpoints Endpoints { get; set; }
}