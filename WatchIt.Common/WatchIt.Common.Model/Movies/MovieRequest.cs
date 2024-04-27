using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Movies;

public class MovieRequest : Movie
{
    #region PUBLIC METHODS

    public Media CreateMedia() => new Media
    {
        Title = Title,
        OriginalTitle = OriginalTitle,
        Description = Description,
        ReleaseDate = ReleaseDate,
        Length = Length,
    };

    public MediaMovie CreateMediaMovie(long id) => new MediaMovie 
    { 
        Id = id,
        Budget = Budget,
    };

    public void UpdateMedia(Media media)
    {
        media.Title = Title;
        media.OriginalTitle = OriginalTitle;
        media.Description = Description;
        media.ReleaseDate = ReleaseDate;
        media.Length = Length;
    }

    public void UpdateMediaMovie(MediaMovie mediaMovie)
    {
        mediaMovie.Budget = Budget;
    }

    #endregion
}