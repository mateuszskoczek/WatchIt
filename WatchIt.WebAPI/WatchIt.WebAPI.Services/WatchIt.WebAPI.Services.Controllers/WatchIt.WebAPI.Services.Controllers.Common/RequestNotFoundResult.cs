using Microsoft.AspNetCore.Mvc;

namespace WatchIt.WebAPI.Services.Controllers.Common;

public class RequestNotFoundResult : RequestResult
{
    #region CONSTRUCTORS
    
    public RequestNotFoundResult() : base(RequestResultStatus.NotFound)
    {
    }
    
    #endregion
    
    
    
    #region CONVERTION

    protected override ActionResult ConvertToActionResult() => new NotFoundResult();

    #endregion
}