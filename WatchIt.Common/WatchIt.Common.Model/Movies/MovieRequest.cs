using System.Diagnostics.CodeAnalysis;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Movies;

public class MovieRequest : Movie
{
    #region CONSTRUCTORS

    [SetsRequiredMembers]
    public MovieRequest(MovieResponse initData)
    {
        Title = initData.Title;
        OriginalTitle = initData.OriginalTitle;
        Description = initData.Description;
        ReleaseDate = initData.ReleaseDate;
        Length = initData.Length;
        Budget = initData.Budget;
    }
    
    public MovieRequest() {}
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public Database.Model.Media.Media CreateMedia() => new Database.Model.Media.Media
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

    public void UpdateMedia(Database.Model.Media.Media media)
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