using Microsoft.AspNetCore.Mvc;

namespace WatchIt.WebAPI.Services.Controllers.Common;

public class RequestOkResult : RequestResult
{
    #region CONSTRUCTORS

    internal RequestOkResult() : base(RequestResultStatus.Ok)
    {
    }
    
    #endregion
    
    
    
    #region CONVERTION

    protected override ActionResult ConvertToActionResult() => new OkResult();

    #endregion
}

public class RequestOkResult<T> : RequestOkResult
{
    #region PROPERTIES
    
    public T Data { get; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    internal RequestOkResult(T data) : base() => Data = data;
    
    #endregion
    
    
    
    #region CONVERTION

    protected override ActionResult ConvertToActionResult() => new OkObjectResult(Data);

    #endregion
}