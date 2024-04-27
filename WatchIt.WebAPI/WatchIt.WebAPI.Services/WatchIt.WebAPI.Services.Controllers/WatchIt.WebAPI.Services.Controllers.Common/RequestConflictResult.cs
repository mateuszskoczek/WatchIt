using Microsoft.AspNetCore.Mvc;

namespace WatchIt.WebAPI.Services.Controllers.Common;

public class RequestConflictResult : RequestResult
{
    #region CONSTRUCTORS
    
    public RequestConflictResult() : base(RequestResultStatus.Conflict)
    {
    }
    
    #endregion
    
    
    
    #region CONVERTION

    protected override ActionResult ConvertToActionResult() => new ConflictResult();

    #endregion
}