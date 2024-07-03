using Microsoft.AspNetCore.Mvc;

namespace WatchIt.WebAPI.Services.Controllers.Common;

public class RequestForbiddenResult : RequestResult
{
    #region CONSTRUCTORS
    
    public RequestForbiddenResult() : base(RequestResultStatus.Forbidden)
    {
    }
    
    #endregion
    
    
    
    #region CONVERTION

    protected override ActionResult ConvertToActionResult() => new ForbidResult();

    #endregion
}