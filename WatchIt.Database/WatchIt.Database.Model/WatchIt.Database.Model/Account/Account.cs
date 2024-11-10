using WatchIt.Database.Model.Common;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Account;

public class Account
{
    #region PROPERTIES

    public long Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string? Description { get; set; }
    public short? GenderId { get; set; }
    public Guid? ProfilePictureId { get; set; }
    public Guid? BackgroundPictureId { get; set; }
    public byte[] Password { get; set; }
    public string LeftSalt { get; set; }
    public string RightSalt { get; set; }
    public bool IsAdmin { get; set; } = false;
    public DateTime CreationDate { get; set; }
    public DateTime LastActive { get; set; }

    #endregion
    
    
    
    #region NAVIGATION

    public virtual Gender? Gender { get; set; }
    public virtual AccountProfilePicture? ProfilePicture { get; set; }
    public virtual MediaPhotoImage? BackgroundPicture { get; set; }

    public virtual IEnumerable<RatingMedia> RatingMedia { get; set; } = new List<RatingMedia>();
    public virtual IEnumerable<RatingPersonActorRole> RatingPersonActorRole { get; set; } = new List<RatingPersonActorRole>();
    public virtual IEnumerable<RatingPersonCreatorRole> RatingPersonCreatorRole { get; set; } = new List<RatingPersonCreatorRole>();
    public virtual IEnumerable<RatingMediaSeriesSeason> RatingMediaSeriesSeason { get; set; } = new List<RatingMediaSeriesSeason>();
    public virtual IEnumerable<RatingMediaSeriesEpisode> RatingMediaSeriesEpisode { get; set; } = new List<RatingMediaSeriesEpisode>();

    public virtual IEnumerable<AccountRefreshToken> AccountRefreshTokens { get; set; } = new List<AccountRefreshToken>();
    
    public virtual IEnumerable<AccountFollow> AccountFollows { get; set; } = new List<AccountFollow>();
    public virtual IEnumerable<AccountFollow> AccountFollowers { get; set; } = new List<AccountFollow>();

    #endregion
}