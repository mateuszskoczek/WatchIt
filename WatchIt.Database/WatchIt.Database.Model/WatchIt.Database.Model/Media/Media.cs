using WatchIt.Database.Model.Common;
using WatchIt.Database.Model.Person;
using WatchIt.Database.Model.Rating;
using WatchIt.Database.Model.ViewCount;

namespace WatchIt.Database.Model.Media;

public class Media
{
    #region PROPERTIES

    public long Id { get; set; }
    public required string Title { get; set; }
    public string? OriginalTitle { get; set; }
    public string? Description { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public short? Length { get; set; }
    public Guid? MediaPosterImageId { get; set; }

    #endregion



    #region NAVIGATION

    public virtual MediaPosterImage? MediaPosterImage { get; set; }

    public virtual IEnumerable<MediaPhotoImage> MediaPhotoImages { get; set; } = new List<MediaPhotoImage>();

    public virtual IEnumerable<MediaGenre> MediaGenres { get; set; } = new List<MediaGenre>();
    public virtual IEnumerable<Genre> Genres { get; set; } = new List<Genre>();

    public virtual IEnumerable<MediaProductionCountry> MediaProductionCountries { get; set; } = new List<MediaProductionCountry>();
    public virtual IEnumerable<Country> ProductionCountries { get; set; } = new List<Country>();

    public virtual IEnumerable<PersonActorRole> PersonActorRoles { get; set; } = new List<PersonActorRole>();

    public virtual IEnumerable<PersonCreatorRole> PersonCreatorRoles { get; set; } = new List<PersonCreatorRole>();

    public virtual IEnumerable<RatingMedia> RatingMedia { get; set; } = new List<RatingMedia>();

    public virtual IEnumerable<ViewCountMedia> ViewCountsMedia { get; set; } = new List<ViewCountMedia>();

    #endregion
}