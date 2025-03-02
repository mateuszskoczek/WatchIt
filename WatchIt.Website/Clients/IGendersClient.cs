using Refit;
using WatchIt.DTO.Models.Controllers.Genders.Gender;
using WatchIt.DTO.Query;

namespace WatchIt.Website.Clients;

public interface IGendersClient
{
    [Get("/")]
    Task<IApiResponse<IEnumerable<GenderResponse>>> GetGenders([Query] GenderFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null);

    [Get("/{id}")]
    Task<IApiResponse<GenderResponse>> GetGender(short id);

    [Post("/")]
    Task<IApiResponse<GenderResponse>> PostGender([Authorize]string token, [Body] GenderRequest body);

    [Delete("/{id}")]
    Task<IApiResponse> DeleteGender([Authorize]string token, short id);
}