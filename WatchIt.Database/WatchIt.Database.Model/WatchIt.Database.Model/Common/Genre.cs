using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Common;

public class Genre
{
    #region PROPERTIES
    
    public short Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    #endregion



    #region NAVIGATION

    public virtual IEnumerable<MediaGenre> MediaGenres { get; set; } = new List<MediaGenre>();
    public virtual IEnumerable<Media.Media> Media { get; set; } = new List<Media.Media>();

    #endregion
}