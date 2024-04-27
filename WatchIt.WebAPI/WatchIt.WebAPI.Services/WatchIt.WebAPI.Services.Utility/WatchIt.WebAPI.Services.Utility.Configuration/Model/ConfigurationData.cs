namespace WatchIt.WebAPI.Services.Utility.Configuration.Model;

public class ConfigurationData
{
    public Logging Logging { get; set; }
    public string AllowedHosts { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
    public RootUser RootUser { get; set; }
    public Authentication Authentication { get; set; }
}