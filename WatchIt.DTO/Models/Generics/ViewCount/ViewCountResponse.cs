namespace WatchIt.DTO.Models.Generics.ViewCount;

public class ViewCountResponse
{
    #region PROPERTIES

    public long Last24Hours { get; set; }
    public long LastWeek { get; set; }
    public long LastMonth { get; set; }
    public long LastYear { get; set; }

    #endregion
}