using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Common.Model.Rating;

public class RatingResponse
{
    #region PROPERTIES
    
    [JsonPropertyName("average")]
    public required decimal Average { get; set; }
    
    [JsonPropertyName("count")]
    public required long Count { get; set; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    [JsonConstructor]
    public RatingResponse() {}

    [SetsRequiredMembers]
    private RatingResponse(long ratingSum, long ratingCount)
    {
        Average = ratingCount > 0 ? (decimal)ratingSum / ratingCount : 0;
        Count = ratingCount;
    }

    public static RatingResponse Create(long ratingSum, long ratingCount) => new RatingResponse(ratingSum, ratingCount);
    
    public static RatingResponse Create(IEnumerable<RatingMedia> ratingMedia) => Create(ratingMedia, x => x.Rating);
    
    public static RatingResponse Create(IEnumerable<RatingPersonActorRole> ratingPersonActorRoles) => Create(ratingPersonActorRoles, x => x.Rating);
    
    public static RatingResponse Create(IEnumerable<RatingPersonCreatorRole> ratingPersonCreatorRoles) => Create(ratingPersonCreatorRoles, x => x.Rating);
    
    public static RatingResponse Create<T>(IEnumerable<T> ratingList, Func<T, short> ratingSelector) => new RatingResponse(ratingList.Sum(x => ratingSelector(x)), ratingList.Count());

    public static RatingResponse Create(IEnumerable<PersonActorRole> personActorRoles, IEnumerable<PersonCreatorRole> personCreatorRoles)
    {
        IEnumerable<RatingPersonActorRole> ratingsActorRoles = personActorRoles.SelectMany(x => x.RatingPersonActorRole);
        IEnumerable<RatingPersonCreatorRole> ratingsCreatorRoles = personCreatorRoles.SelectMany(x => x.RatingPersonCreatorRole);
        return Create(ratingsActorRoles, ratingsCreatorRoles);
    }

    public static RatingResponse Create(IEnumerable<RatingPersonActorRole> ratingsActorRoles, IEnumerable<RatingPersonCreatorRole> ratingsCreatorRoles)
    {
        long ratingSum = ratingsActorRoles.Sum(x => x.Rating) + ratingsCreatorRoles.Sum(x => x.Rating);
        long ratingCount = ratingsActorRoles.Count() + ratingsCreatorRoles.Count(); 
        return new RatingResponse(ratingSum, ratingCount);
    }

    #endregion
}