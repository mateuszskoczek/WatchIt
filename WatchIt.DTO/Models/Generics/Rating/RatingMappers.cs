using WatchIt.Database.Model;

namespace WatchIt.DTO.Models.Generics.Rating;

public static class RatingMappers
{
    #region PUBLIC METHODS
    
    public static void UpdateWithRequest(this IRatingEntity entity, RatingRequest ratingRequest)
    {
        entity.Rating = ratingRequest.Rating;
    }
    
    public static RatingOverallResponse ToOverallResponse(this IEnumerable<IRatingEntity> entities)
    {
        IEnumerable<IRatingEntity> ratingEntities = entities.ToList();
        
        long sum = ratingEntities.Sum(x => x.Rating);
        long count = ratingEntities.Count();
        
        return new RatingOverallResponse
        {
            Rating = count > 0 ? (decimal)sum / count : null,
            Count = count
        };
    }

    public static RatingUserResponse ToUserResponse(this IRatingEntity entity) => new RatingUserResponse
    {
        Date = entity.Date,
        Rating = entity.Rating,
    };

    public static RatingUserOverallResponse ToUserOverallResponse(this IEnumerable<IRatingEntity> entities)
    {
        IEnumerable<IRatingEntity> ratingEntities = entities.ToList();
        
        long sum = ratingEntities.Sum(x => x.Rating);
        long count = ratingEntities.Count();
        DateTimeOffset? lastDate = count == 0 ? null : ratingEntities.Max(x => x.Date);
        
        return new RatingUserOverallResponse
        {
            Rating = count > 0 ? (decimal)sum / count : null,
            Date = lastDate,
            Count = count
        };
    }
    
    #endregion
}