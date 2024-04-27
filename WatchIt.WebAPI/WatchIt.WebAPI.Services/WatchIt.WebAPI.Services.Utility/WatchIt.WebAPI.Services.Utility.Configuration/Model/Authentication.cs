namespace WatchIt.WebAPI.Services.Utility.Configuration.Model;

public class Authentication
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public Tokens Tokens { get; set; }
}