using System.Text;
using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Media;

public class MediaPhotoQueryParameters : QueryParameters<MediaPhotoResponse>
{
    #region PROPERTIES

    [FromQuery(Name = "mime_type")]
    public string? MimeType { get; set; }
    
    [FromQuery(Name = "is_background")]
    public bool? IsBackground { get; set; }
    
    [FromQuery(Name = "is_universal_background")]
    public bool? IsUniversalBackground { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public override bool IsMeetingConditions(MediaPhotoResponse item) =>
    (
        TestString(item.MimeType, MimeType)
        &&
        TestBoolean(item.Background is not null, IsBackground)
        &&
        TestBoolean(item.Background!.IsUniversalBackground, IsUniversalBackground)
    );

    #endregion
}