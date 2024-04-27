using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WatchIt.WebAPI.Services.Controllers.Common;

public class RequestUnauthorizedResult : RequestResult
{
    #region CONSTRUCTORS

    public RequestUnauthorizedResult() : base(RequestResultStatus.Unauthorized)
    {
    }

    #endregion

    
    
    #region CONVERTION

    protected override ActionResult ConvertToActionResult() => new UnauthorizedResult();
    
    #endregion
}