namespace WatchIt.Common.Model.Roles;

public interface IRoleResponse
{
    Guid Id { get; set; }
    long MediaId { get; set; }
    long PersonId { get; set; }
    short TypeId { get; set; }
}