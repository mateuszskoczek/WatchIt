using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Common;

public class Country
{
    #region PROPERTIES

    public short Id { get; set; }
    public required string Name { get; set; }
    public bool IsHistorical { get; set; }

    #endregion



    #region NAVIGATION

    public virtual IEnumerable<MediaProductionCountry> MediaProductionCountries { get; set; } = new List<MediaProductionCountry>();
    public virtual IEnumerable<Media.Media> MediaProduction { get; set; } = new List<Media.Media>();

    #endregion
}