namespace WatchIt.DTO.Models.Controllers.Roles.Role.Query;

public interface IRoleFilterQuery
{
    short? TypeId { get; set; }
    long? PersonId { get; set; }
    long? MediumId { get; set; }
}