using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Controllers.Photos.PhotoBackground;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.BusinessLogic.Photos;
using WatchIt.WebAPI.Constants;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("photos")]
public class PhotosController(IPhotosBusinessLogic photosBusinessLogic) : ControllerBase
{
    #region Main

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<PhotoResponse>> GetPhoto([FromRoute(Name = "id")] Guid id) =>
        await photosBusinessLogic.GetPhoto(id);

    [HttpGet]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<PhotoResponse>>> GetPhotos([FromQuery] PhotoFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery) =>
        await photosBusinessLogic.GetPhotos(filterQuery, orderQuery, pagingQuery);

    [HttpPost]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<PhotoResponse>> PostPhoto([FromBody] PhotoRequest body) =>
        await photosBusinessLogic.PostPhoto(body);

    [HttpPut("{id:guid}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<PhotoResponse>> PutPhoto([FromRoute(Name = "id")] Guid id, [FromBody] PhotoRequest body) =>
        await photosBusinessLogic.PutPhoto(id, body);

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeletePhoto([FromRoute(Name = "id")] Guid id) =>
        await photosBusinessLogic.DeletePhoto(id);

    #endregion

    #region Background

    [HttpGet("background")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<PhotoResponse>> GetPhotoBackground() =>
        await photosBusinessLogic.GetPhotoBackground();

    [HttpPut("{photo_id:guid}/background")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<PhotoBackgroundResponse>> PutPhotoBackground([FromRoute(Name = "photo_id")] Guid photoId, [FromBody] PhotoBackgroundRequest body) =>
        await photosBusinessLogic.PutPhotoBackground(photoId, body);

    [HttpDelete("{photo_id:guid}/background")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeletePhotoBackground([FromRoute(Name = "photo_id")] Guid photoId) =>
        await photosBusinessLogic.DeletePhotoBackground(photoId);

    #endregion
}