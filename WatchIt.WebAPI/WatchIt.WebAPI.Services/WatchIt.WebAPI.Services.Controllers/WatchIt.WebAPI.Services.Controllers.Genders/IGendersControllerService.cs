using WatchIt.Common.Model.Genders;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Genders;

public interface IGendersControllerService
{
    Task<RequestResult> GetAllGenders(GenderQueryParameters query);
    Task<RequestResult> GetGender(short id);
    Task<RequestResult> PostGender(GenderRequest data);
    Task<RequestResult> DeleteGender(short id);
}