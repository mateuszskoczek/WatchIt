using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Model.Genders;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Query;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Common.Model.Persons;

public class PersonRatedResponse : PersonResponse, IQueryOrderable<PersonRatedResponse>
{
    #region PROPERTIES
    
    [JsonIgnore]
    public static IDictionary<string, Func<PersonRatedResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<PersonRatedResponse, IComparable>>
    {
        { "id", x => x.Id },
        { "name", x => x.Name },
        { "full_name", x => x.FullName },
        { "description", x => x.Description },
        { "birth_date", x => x.BirthDate },
        { "death_date", x => x.BirthDate },
        { "gender", x => x.Gender.Name },
        { "rating.average", x => x.Rating.Average },
        { "rating.count", x => x.Rating.Count },
        { "user_rating.average", x => x.UserRating.Average },
        { "user_rating.count", x => x.UserRating.Count },
        { "user_rating_last_date", x => x.UserRatingLastDate }
    };
    
    [JsonPropertyName("user_rating")]
    public RatingResponse UserRating { get; set; }

    [JsonPropertyName("user_rating_last_date")]
    public DateTime UserRatingLastDate { get; set; }
    
    #endregion



    #region CONSTRUCTORS

    [JsonConstructor]
    public PersonRatedResponse() { }

    [SetsRequiredMembers]
    public PersonRatedResponse(Database.Model.Person.Person person, IEnumerable<RatingPersonActorRole> actorUserRatings, IEnumerable<RatingPersonCreatorRole> creatorUserRatings)
    {
        Id = person.Id;
        Name = person.Name;
        FullName = person.FullName;
        Description = person.Description;
        BirthDate = person.BirthDate;
        DeathDate = person.DeathDate;
        Gender = person.Gender is not null ? new GenderResponse(person.Gender) : null;
        Rating = RatingResponseBuilder.Initialize()
                                      .Add(person.PersonActorRoles.SelectMany(x => x.RatingPersonActorRole), x => x.Rating)
                                      .Add(person.PersonCreatorRoles.SelectMany(x => x.RatingPersonCreatorRole), x => x.Rating)
                                      .Build();
        UserRating = RatingResponseBuilder.Initialize()
                                          .Add(actorUserRatings, x => x.Rating)
                                          .Add(creatorUserRatings, x => x.Rating)
                                          .Build();
        UserRatingLastDate = actorUserRatings.Select(x => x.Date)
                                             .Union(creatorUserRatings.Select(x => x.Date))
                                             .Max();
    }

    #endregion
}