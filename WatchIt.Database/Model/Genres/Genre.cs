using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Genres;

public class Genre
{
    #region PROPERTIES

    public short Id { get; set; }
    public string Name { get; set; } = default!;
    public uint Version { get; set; }

    #endregion
    
    
    
    #region NAVIGATION
    
    // Media
    public virtual IEnumerable<MediumGenre> MediaRelationObjects { get; set; } = new List<MediumGenre>();
    public virtual IEnumerable<Medium> Media { get; set; } = new List<Medium>();
    
    #endregion
}