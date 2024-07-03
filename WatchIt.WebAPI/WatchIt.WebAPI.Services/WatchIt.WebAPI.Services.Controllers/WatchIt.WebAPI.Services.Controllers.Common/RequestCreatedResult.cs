using Microsoft.AspNetCore.Mvc;

namespace WatchIt.WebAPI.Services.Controllers.Common;

public class RequestCreatedResult<T> : RequestResult
{
    #region PROPERTIES
    
    public string Location { get; }
    public T Data { get; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    internal RequestCreatedResult(string location, T data) : base(RequestResultStatus.Created)
    {
        Location = location;
        Data = data;
    }

    #endregion
    
    
    
    #region CONVERTION

    protected override ActionResult ConvertToActionResult() => new CreatedResult(Location, Data);

    #endregion
}