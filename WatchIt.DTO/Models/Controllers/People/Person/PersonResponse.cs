using WatchIt.DTO.Models.Controllers.Genders.Gender;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Models.Generics.ViewCount;

namespace WatchIt.DTO.Models.Controllers.People.Person;

public class PersonResponse
{
    #region PROPERTIES
    
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? FullName { get; set; }
    public string? Description { get; set; }
    public DateOnly? BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public GenderResponse? Gender { get; set; }
    public RatingOverallResponse Rating { get; set; } = null!;
    public ViewCountResponse ViewCount { get; set; } = null!;
    public ImageResponse? Picture { get; set; }

    #endregion
}