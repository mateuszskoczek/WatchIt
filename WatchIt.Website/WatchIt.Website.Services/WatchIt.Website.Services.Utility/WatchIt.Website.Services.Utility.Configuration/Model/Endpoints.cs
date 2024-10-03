namespace WatchIt.Website.Services.Utility.Configuration.Model;

public class Endpoints
{
    public string Base { get; set; }
    public Accounts Accounts { get; set; }
    public Genres Genres { get; set; }
    public Media Media { get; set; }
    public Movies Movies { get; set; }
    public Series Series { get; set; }
    public Photos Photos { get; set; }
    public Persons Persons { get; set; }
}