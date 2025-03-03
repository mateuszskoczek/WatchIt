using WatchIt.DTO.Models.Controllers.Genders.Gender;

namespace WatchIt.DTO.Models.Controllers.Genders;

public static class GendersMappers
{
    public static GenderResponse ToResponse(this Database.Model.Genders.Gender entity) => new GenderResponse
    {
        Id = entity.Id,
        Name = entity.Name,
    };

    public static Database.Model.Genders.Gender ToEntity(this GenderRequest request) => new Database.Model.Genders.Gender
    {
        Name = request.Name,
    };
}