using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Model.Photos;
using WatchIt.WebAPI.Services.Controllers.Photos;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("photos")]
public class PhotosController : ControllerBase
{
    #region FIELDS

    private readonly IPhotosControllerService _photosControllerService;

    #endregion



    #region CONSTRUCTORS

    public PhotosController(IPhotosControllerService photosControllerService)
    {
        _photosControllerService = photosControllerService;
    }

    #endregion
    
    
    
    #region METHODS
    
    #region Main
    
    [HttpGet("random_background")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPhotoRandomBackground() => await _photosControllerService.GetPhotoRandomBackground();
    
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePhoto([FromRoute] Guid id) => await _photosControllerService.DeletePhoto(id);
    
    #endregion

    #region Background data

    [HttpPut("{id}/background_data")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PutPhotoBackgroundData([FromRoute]Guid id, [FromBody] PhotoBackgroundDataRequest body) => await _photosControllerService.PutPhotoBackgroundData(id, body);
    
    [HttpDelete("{id}/background_data")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePhotoBackgroundData([FromRoute]Guid id) => await _photosControllerService.DeletePhotoBackgroundData(id);

    #endregion
    
    #endregion
}