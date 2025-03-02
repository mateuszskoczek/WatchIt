namespace WatchIt.DTO.Models.Controllers.Media.Medium.Request;

public abstract class MediumRequest
{
    #region PROPERTIES
    
    public string Title { get; set; } = null!;
    public string? OriginalTitle { get; set; }
    public string? Description { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public short? Duration { get; set; }
    
    #endregion
}