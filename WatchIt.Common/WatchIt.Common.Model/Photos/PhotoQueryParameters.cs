using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Photos;

public class PhotoQueryParameters : QueryParameters<PhotoResponse>
{
    #region PROPERTIES

    [FromQuery(Name = "mime_type")]
    public string? MimeType { get; set; }
    
    [FromQuery(Name = "is_background")]
    public bool? IsBackground { get; set; }
    
    [FromQuery(Name = "is_universal_background")]
    public bool? IsUniversalBackground { get; set; }

    [FromQuery(Name = "upload_date")]
    public DateOnly? UploadDate { get; set; }

    [FromQuery(Name = "upload_date_from")]
    public DateOnly? UploadDateFrom { get; set; }

    [FromQuery(Name = "upload_date_to")]
    public DateOnly? UploadDateTo { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public override bool IsMeetingConditions(PhotoResponse item) =>
    (
        TestString(item.MimeType, MimeType)
        &&
        TestBoolean(item.Background is not null, IsBackground)
        &&
        TestBoolean(item.Background is not null && item.Background.IsUniversalBackground, IsUniversalBackground)
        &&
        TestComparable(item.UploadDate, UploadDate, UploadDateFrom, UploadDateTo)
    );

    #endregion
}