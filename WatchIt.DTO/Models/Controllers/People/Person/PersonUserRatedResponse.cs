using WatchIt.DTO.Models.Generics.Rating;

namespace WatchIt.DTO.Models.Controllers.People.Person;

public class PersonUserRatedResponse : PersonResponse
{
    #region PROPERTIES

    public RatingUserOverallResponse RatingUser { get; set; } = null!;

    #endregion
}