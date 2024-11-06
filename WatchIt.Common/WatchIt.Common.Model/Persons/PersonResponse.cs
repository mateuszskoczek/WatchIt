using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Model.Genders;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Query;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Common.Model.Persons;

public class PersonResponse : Person, IQueryOrderable<PersonResponse>
{
    #region PROPERTIES

    [JsonIgnore]
    public static IDictionary<string, Func<PersonResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<PersonResponse, IComparable>>
    {
        { "id", x => x.Id },
        { "name", x => x.Name },
        { "full_name", x => x.FullName },
        { "description", x => x.Description },
        { "birth_date", x => x.BirthDate },
        { "death_date", x => x.BirthDate },
        { "gender", x => x.Gender.Name },
        { "rating.average", x => x.Rating.Average },
        { "rating.count", x => x.Rating.Count }
    };

    
    [JsonPropertyName("id")]
    public required long Id { get; set; }
    
    [JsonPropertyName("gender")]
    public GenderResponse? Gender { get; set; }
    
    [JsonPropertyName("rating")]
    public required RatingResponse Rating { get; set; }

    #endregion



    #region CONSTRUCTORS

    [JsonConstructor]
    public PersonResponse() { }
    
    [SetsRequiredMembers]
    public PersonResponse(Database.Model.Person.Person person)
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
    }

    #endregion
}