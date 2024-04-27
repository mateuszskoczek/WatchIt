using WatchIt.Database.Model.Common;

namespace WatchIt.Database.Model.Media;

public class MediaGenre
{
    #region PROPERTIES

    public required long MediaId { get; set; }
    public required short GenreId { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Media Media { get; set; } = null!;
    public virtual Genre Genre { get; set; } = null!;

    #endregion
}