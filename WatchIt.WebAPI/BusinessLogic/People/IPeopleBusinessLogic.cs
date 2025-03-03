using Ardalis.Result;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.People.Person.Query;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.BusinessLogic.People;

public interface IPeopleBusinessLogic
{
    Task<Result<IEnumerable<PersonResponse>>> GetPeople(PersonFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures);
    Task<Result<PersonResponse>> GetPerson(long personId, bool includePictures);
    Task<Result<PersonResponse>> PostPerson(PersonRequest body);
    Task<Result<PersonResponse>> PutPerson(long personId, PersonRequest body);
    Task<Result> DeletePerson(long personId);
    Task<Result<RatingOverallResponse>> GetPersonRating(long personId);
    Task<Result<RatingUserOverallResponse>> GetPersonUserRating(long personId, long accountId);
    Task<Result> PutPeopleViewCount(long personId);
    Task<Result<ImageResponse>> GetPersonPicture(long personId);
    Task<Result<ImageResponse>> PutPersonPicture(long personId, ImageRequest body);
    Task<Result> DeletePersonPicture(long personId);
}