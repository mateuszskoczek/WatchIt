using WatchIt.Database.Model.Media;
using WatchIt.DTO.Models.Controllers.Genres;
using WatchIt.DTO.Models.Controllers.Media.Medium.Request;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Models.Generics.ViewCount;
using MediumType = WatchIt.Database.Model.Media.MediumType;

namespace WatchIt.DTO.Models.Controllers.Media;

public static class MediaMappers
{
    #region PUBLIC METHODS
    
    #region Medium

    public static MediumMovie ToEntity(this MediumMovieRequest request)
    {
        MediumMovie medium = new MediumMovie();
        medium.UpdateWithRequest(request);
        return medium;
    }

    public static void UpdateWithRequest(this MediumMovie entity, MediumMovieRequest request)
    {
        entity.SetMediumEntityProperties(request);
        entity.Budget = request.Budget;
    }
    
    public static MediumSeries ToEntity(this MediumSeriesRequest request)
    {
        MediumSeries medium = new MediumSeries();
        medium.UpdateWithRequest(request);
        return medium;
    }
    
    public static void UpdateWithRequest(this MediumSeries entity, MediumSeriesRequest request)
    {
        entity.SetMediumEntityProperties(request);
        entity.HasEnded = request.HasEnded;
    }

    public static MediumResponse ToResponse(this Database.Model.Media.Medium entity)
    { 
        MediumResponse response = new MediumResponse();
        response.SetMediumResponseProperties(entity);
        response.Type = entity.Type == MediumType.Movie ? Medium.Response.MediumResponseType.Movie : Medium.Response.MediumResponseType.Series;
        return response;
    }

    public static MediumMovieResponse ToResponse(this MediumMovie entity)
    { 
        MediumMovieResponse response = new MediumMovieResponse();
        response.SetMediumResponseProperties(entity);
        response.SetMediumMovieResponseProperties(entity);
        return response;
    }
    
    public static MediumSeriesResponse ToResponse(this MediumSeries entity)
    { 
        MediumSeriesResponse response = new MediumSeriesResponse();
        response.SetMediumResponseProperties(entity);
        response.SetMediumSeriesResponseProperties(entity);
        return response;
    }
    
    public static MediumUserRatedResponse ToResponse(this Database.Model.Media.Medium entity, long accountId)
    { 
        MediumUserRatedResponse response = new MediumUserRatedResponse();
        response.SetMediumResponseProperties(entity);
        response.SetMediumUserRatedResponseProperties(entity, accountId);
        return response;
    }

    public static MediumMovieUserRatedResponse ToResponse(this MediumMovie entity, long accountId)
    { 
        MediumMovieUserRatedResponse response = new MediumMovieUserRatedResponse();
        response.SetMediumResponseProperties(entity);
        response.SetMediumMovieResponseProperties(entity);
        response.SetMediumUserRatedResponseProperties(entity, accountId);
        return response;
    }
    
    public static MediumSeriesUserRatedResponse ToResponse(this MediumSeries entity, long accountId)
    { 
        MediumSeriesUserRatedResponse response = new MediumSeriesUserRatedResponse();
        response.SetMediumResponseProperties(entity);
        response.SetMediumSeriesResponseProperties(entity);
        response.SetMediumUserRatedResponseProperties(entity, accountId);
        return response;
    }

    public static MediumRequest ToRequest(this BaseMediumResponse response)
    {
        MediumRequest request = response switch
        {
            MediumMovieResponse mediumMovieResponse => new MediumMovieRequest
            {
                Budget = mediumMovieResponse.Budget,
            },
            MediumSeriesResponse mediumSeriesResponse => new MediumSeriesRequest
            {
                HasEnded = mediumSeriesResponse.HasEnded,
            }
        };
        request.Title = response.Title;
        request.Description = response.Description;
        request.OriginalTitle = response.OriginalTitle;
        request.ReleaseDate = response.ReleaseDate;
        request.Duration = response.Duration;
        return request;
    }
    
    #endregion
    
    #region MediumPicture

    public static MediumPicture ToEntity(this ImageRequest request, long mediumId) => new Database.Model.Media.MediumPicture
    {
        MediumId = mediumId,
        Image = request.Image,
        MimeType = request.MimeType,
    };

    #endregion
    
    #region MediumGenre

    public static MediumGenre CreateMediumGenre(long mediumId, short genreId) => new MediumGenre
    {
        MediumId = mediumId,
        GenreId = genreId,
    };
    
    #endregion
    
    #region MediumRating

    public static MediumRating ToEntity(this RatingRequest request, long mediumId, long userId)
    {
        MediumRating entity = new MediumRating
        {
            MediumId = mediumId,
            AccountId = userId
        };
        entity.UpdateWithRequest(request);
        return entity;
    }
    
    #endregion

    #region MediumViewCount

    public static MediumViewCount CreateMediumViewCountEntity(long mediumId) => new MediumViewCount
    {
        MediumId = mediumId,
        ViewCount = 1,
    };

    #endregion
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    private static void SetMediumEntityProperties(this Database.Model.Media.Medium entity, MediumRequest request)
    {
        entity.Title = request.Title;
        entity.OriginalTitle = request.OriginalTitle;
        entity.Description = request.Description;
        entity.Duration = request.Duration;
        entity.ReleaseDate = request.ReleaseDate;
    }

    private static void SetMediumResponseProperties(this BaseMediumResponse response, Database.Model.Media.Medium entity)
    {
        response.Id = entity.Id;
        response.Title = entity.Title;
        response.OriginalTitle = entity.OriginalTitle;
        response.Description = entity.Description;
        response.ReleaseDate = entity.ReleaseDate;
        response.Duration = entity.Duration;
        response.Genres = entity.Genres.Select(x => x.ToResponse());
        response.Rating = entity.Ratings.ToOverallResponse();
        response.ViewCount = entity.ViewCounts.ToResponse();
        response.Picture = entity.Picture?.ToResponse();
    }
    
    private static void SetMediumMovieResponseProperties(this MediumMovieResponse response, MediumMovie entity)
    {
        response.Budget = entity.Budget;
    }
    
    private static void SetMediumSeriesResponseProperties(this MediumSeriesResponse response, MediumSeries entity)
    {
        response.HasEnded = entity.HasEnded;
    }

    private static void SetMediumUserRatedResponseProperties(this IMediumUserRatedResponse response, Database.Model.Media.Medium entity, long accountId)
    {
        response.RatingUser = entity.Ratings.SingleOrDefault(x => x.AccountId == accountId)?.ToUserResponse();
    }
    
    #endregion
}