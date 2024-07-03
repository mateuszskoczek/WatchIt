namespace WatchIt.Database.Model.Media;

public class MediaPhotoImageBackground
{
    #region PROPERTIES
    
    public required Guid Id { get; set; }
    public required bool IsUniversalBackground { get; set; }
    public required byte[] FirstGradientColor { get; set; }
    public required byte[] SecondGradientColor { get; set; }
    
    #endregion



    #region NAVIGATION

    public virtual MediaPhotoImage MediaPhotoImage { get; set; } = null!;

    #endregion
}