using WatchIt.Database.Model.Common;

namespace WatchIt.Database.Model.Media;

public class MediaProductionCountry
{
    #region PROPERTIES

    public required long MediaId { get; set; }
    public required short CountryId { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Media Media { get; set; } = null!;
    public virtual Country Country { get; set; } = null!;

    #endregion
}