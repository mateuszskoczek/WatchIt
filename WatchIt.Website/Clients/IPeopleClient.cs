using Refit;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.People.Person.Query;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;

namespace WatchIt.Website.Clients;

public interface IPeopleClient
{
    #region Main

    [Get("/")]
    Task<IApiResponse<IEnumerable<PersonResponse>>> GetPeople([Query] PersonFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null, [Query][AliasAs("include_pictures")] bool includePictures = false);

    [Get("/{id}")]
    Task<IApiResponse<PersonResponse>> GetPerson(long id, [Query][AliasAs("include_pictures")] bool includePictures = false);

    [Post("/")]
    Task<IApiResponse<PersonResponse>> PostPerson([Authorize]string token, [Body] PersonRequest body);

    [Put("/{id}")]
    Task<IApiResponse<PersonResponse>> PutPerson([Authorize]string token, long id, [Body] PersonRequest body);

    [Delete("/{id}")]
    Task<IApiResponse> DeletePerson([Authorize]string token, long id);

    #endregion

    #region Rating

    [Get("/{id}/rating")]
    Task<IApiResponse<RatingOverallResponse>> GetPersonRating(long id);

    [Get("/{id}/rating/{account_id}")]
    Task<IApiResponse<RatingUserOverallResponse>> GetPersonUserRating(long id, [AliasAs("account_id")] long accountId);

    #endregion

    #region View Count

    [Put("/{id}/view_count")]
    Task<IApiResponse> PutPeopleViewCount(long id);

    #endregion

    #region Picture

    [Get("/{id}/picture")]
    Task<IApiResponse<ImageResponse>> GetPersonPicture(long id);

    [Put("/{id}/picture")]
    Task<IApiResponse<ImageResponse>> PutPersonPicture([Authorize]string token, long id, [Body] ImageRequest body);

    [Delete("/{id}/picture")]
    Task<IApiResponse> DeletePersonPicture([Authorize]string token, long id);

    #endregion
}