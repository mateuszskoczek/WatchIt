using Ardalis.Result;
using WatchIt.DTO.Models.Controllers.Genders.Gender;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.BusinessLogic.Genders;

public interface IGendersBusinessLogic
{
    #region PUBLIC METHODS

    Task<Result<IEnumerable<GenderResponse>>> GetGenders(GenderFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery);
    Task<Result<GenderResponse>> GetGender(short genderId);
    Task<Result<GenderResponse>> PostGender(GenderRequest body);
    Task<Result> DeleteGender(short genderId);

    #endregion
}