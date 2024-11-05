namespace WatchIt.Website.Services.Configuration.Model;

public class Accounts
{
    public string Base { get; set; }
    public string Register { get; set; }
    public string Authenticate { get; set; }
    public string AuthenticateRefresh { get; set; }
    public string Logout { get; set; }
    public string GetAccountProfilePicture { get; set; }
    public string PutAccountProfilePicture { get; set; }
    public string DeleteAccountProfilePicture { get; set; }
    public string GetAccountProfileBackground { get; set; }
    public string PutAccountProfileBackground { get; set; }
    public string DeleteAccountProfileBackground { get; set; }
    public string GetAccountInfo { get; set; }
    public string PutAccountProfileInfo { get; set; }
    public string PatchAccountUsername { get; set; }
    public string GetAccountRatedMovies { get; set; }
    public string GetAccountRatedSeries { get; set; }
    public string GetAccountRatedPersons { get; set; }
}