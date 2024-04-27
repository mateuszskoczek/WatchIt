using Microsoft.AspNetCore.Mvc;

namespace WatchIt.WebAPI.Services.Controllers.Common;

public class RequestNoContentResult : RequestResult
{
    #region CONSTRUCTORS

    internal RequestNoContentResult() : base(RequestResultStatus.NoContent)
    {
    }

    #endregion
    
    
    
    #region CONVERTION

    protected override ActionResult ConvertToActionResult() => new NoContentResult();

    #endregion
}