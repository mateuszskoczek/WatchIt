using WatchIt.Database.Model.Genres;
using WatchIt.Database.Model.Photos;
using WatchIt.Database.Model.Roles;

namespace WatchIt.Database.Model.Media;

public abstract class Medium
{
    #region PROPERTIES
    
    public long Id { get; set; }
    public MediumType Type { get; set; }
    public string Title { get; set; } = default!;
    public string? OriginalTitle { get; set; }
    public string? Description { get; set; }
    public short? Duration { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public uint Version { get; set; }
    
    #endregion



    #region NAVIGATION

    // Genres
    public virtual IEnumerable<MediumGenre> GenresRelationshipObjects { get; set; } = new List<MediumGenre>();
    public virtual IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
    
    // Picture
    public virtual MediumPicture? Picture { get; set; }
    
    // Photos
    public virtual IEnumerable<Photo> Photos { get; set; } = new List<Photo>();
    
    // View counts
    public virtual IEnumerable<MediumViewCount> ViewCounts { get; set; } = new List<MediumViewCount>();
    
    // Ratings
    public virtual IEnumerable<MediumRating> Ratings { get; set; } = new List<MediumRating>();
    public virtual IEnumerable<Accounts.Account> RatedBy { get; set; } = new List<Accounts.Account>();
    
    // Roles
    public virtual IEnumerable<Role> Roles { get; set; } = new List<Role>();

    #endregion
}