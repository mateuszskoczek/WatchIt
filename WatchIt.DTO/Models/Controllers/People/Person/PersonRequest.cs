namespace WatchIt.DTO.Models.Controllers.People.Person;

public class PersonRequest
{
    #region PROPERTIES
    
    public string Name { get; set; } = null!;
    public string? FullName { get; set; }
    public string? Description { get; set; }
    public DateOnly? BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public short? GenderId { get; set; }
    
    #endregion
}