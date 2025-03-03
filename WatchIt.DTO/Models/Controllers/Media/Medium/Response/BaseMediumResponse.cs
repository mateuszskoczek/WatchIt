using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Models.Generics.ViewCount;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Response;

public abstract class BaseMediumResponse
{
    #region PROPERTIES

    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string? OriginalTitle { get; set; }
    public string? Description { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public short? Duration { get; set; }
    public IEnumerable<GenreResponse> Genres { get; set; } = null!;
    public RatingOverallResponse Rating { get; set; } = null!;
    public ViewCountResponse ViewCount { get; set; } = null!;
    public ImageResponse? Picture { get; set; }

    #endregion
}