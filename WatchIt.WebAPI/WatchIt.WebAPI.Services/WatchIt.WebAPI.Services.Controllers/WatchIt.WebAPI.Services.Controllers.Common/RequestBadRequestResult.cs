using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WatchIt.WebAPI.Services.Controllers.Common;

public class RequestBadRequestResult : RequestResult
{
    #region FIELDS

    private readonly ModelStateDictionary _modelState;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    public RequestBadRequestResult() : base(RequestResultStatus.BadRequest)
    {
        _modelState = new ModelStateDictionary();
    }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public RequestBadRequestResult AddValidationError(string propertyName, string message)
    {
        _modelState.AddModelError(propertyName, message);
        return this;
    }
    
    #endregion

    
    
    #region CONVERTION

    protected override ActionResult ConvertToActionResult() => new BadRequestObjectResult(_modelState);
    
    #endregion
}