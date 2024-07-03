using Microsoft.AspNetCore.Mvc;

namespace WatchIt.WebAPI.Services.Controllers.Common;

public abstract class RequestResult
{
    #region PROPERTIES

    public RequestResultStatus Status { get; }

    #endregion



    #region CONSTRUCTORS

    protected RequestResult(RequestResultStatus status) => Status = status;
    
    public static RequestOkResult Ok() => new RequestOkResult();
    public static RequestOkResult<T> Ok<T>(T data) => new RequestOkResult<T>(data);
    public static RequestCreatedResult<T> Created<T>(string location, T data) => new RequestCreatedResult<T>(location, data);
    public static RequestNoContentResult NoContent() => new RequestNoContentResult();
    public static RequestBadRequestResult BadRequest() => new RequestBadRequestResult();
    public static RequestUnauthorizedResult Unauthorized() => new RequestUnauthorizedResult();
    public static RequestForbiddenResult Forbidden() => new RequestForbiddenResult();
    public static RequestNotFoundResult NotFound() => new RequestNotFoundResult();
    public static RequestConflictResult Conflict() => new RequestConflictResult();

    #endregion
    
    
    
    #region CONVERSION

    public static implicit operator ActionResult(RequestResult result) => result.ConvertToActionResult();

    protected abstract ActionResult ConvertToActionResult();

    #endregion
}