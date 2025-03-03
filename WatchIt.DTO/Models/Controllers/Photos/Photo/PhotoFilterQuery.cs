using Microsoft.AspNetCore.Mvc;
using Refit;
using WatchIt.DTO.Models.Controllers.Photos.Photo.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Photos.Photo;

public class PhotoFilterQuery : IFilterQuery<Database.Model.Photos.Photo>
{
    #region PROPERTIES
    
    [FromQuery(Name = "medium_id")]
    [AliasAs("medium_id")]
    public long? MediumId { get; set; }
    
    [FromQuery(Name = "mime_type")]
    [AliasAs("mime_type")]
    public string? MimeType { get; set; }
    
    [FromQuery(Name = "is_background")]
    [AliasAs("is_background")]
    public bool? IsBackground { get; set; }
    
    [FromQuery(Name = "is_universal_background")]
    [AliasAs("is_universal_background")]
    public bool? IsUniversalBackground { get; set; }

    [FromQuery(Name = "upload_date_from")]
    [AliasAs("upload_date_from")]
    public DateOnly? UploadDateFrom { get; set; }

    [FromQuery(Name = "upload_date_to")]
    [AliasAs("upload_date_to")]
    public DateOnly? UploadDateTo { get; set; }

    #endregion

    
    
    #region PUBLIC METHODS
    
    public IEnumerable<Filter<Database.Model.Photos.Photo>> GetFilters() =>
    [
        new PhotoMediumIdFilter(MediumId),
        new PhotoMimeTypeFilter(MimeType),
        new PhotoIsBackgroundFilter(IsBackground),
        new PhotoIsUniversalBackgroundFilter(IsUniversalBackground),
        new PhotoUploadDateFromFilter(UploadDateFrom),
        new PhotoUploadDateToFilter(UploadDateTo),
    ];
    
    #endregion
}