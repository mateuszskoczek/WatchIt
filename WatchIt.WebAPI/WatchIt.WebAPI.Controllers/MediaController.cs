using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.WebAPI.Services.Controllers.Media;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("media")]
public class MediaController(IMediaControllerService mediaControllerService)
{
    [HttpGet("{id}/genres")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<GenreResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetGenres([FromRoute]long id) => await mediaControllerService.GetGenres(id);
    
    [HttpPost("{id}/genres/{genre_id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostGenre([FromRoute]long id, [FromRoute(Name = "genre_id")]short genreId) => await mediaControllerService.PostGenre(id, genreId);
    
    [HttpDelete("{id}/genres/{genre_id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteGenre([FromRoute]long id, [FromRoute(Name = "genre_id")]short genreId) => await mediaControllerService.DeleteGenre(id, genreId);
    
    [HttpGet("{id}/photos/random_background")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaPhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaRandomBackgroundPhoto([FromRoute]long id) => await mediaControllerService.GetMediaRandomBackgroundPhoto(id);

    [HttpGet("{id}/poster")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaPosterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPoster([FromRoute] long id) => await mediaControllerService.GetPoster(id);
    
    [HttpPut("{id}/poster")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PutPoster([FromRoute]long id, [FromBody]MediaPosterRequest body) => await mediaControllerService.PutPoster(id, body);
    
    [HttpDelete("{id}/poster")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeletePoster([FromRoute]long id) => await mediaControllerService.DeletePoster(id);
    
    [HttpGet("photos/{photo_id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaPhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPhoto([FromRoute(Name = "photo_id")] Guid photoId) => await mediaControllerService.GetPhoto(photoId);

    [HttpGet("photos")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<MediaPhotoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetPhotos(MediaPhotoQueryParameters query) => await mediaControllerService.GetPhotos(query);

    [HttpGet("photos/random_background")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaPhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetRandomBackgroundPhoto() => await mediaControllerService.GetRandomBackgroundPhoto();
    
    [HttpPost("photos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(MediaPhotoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PostPhoto([FromBody]MediaPhotoRequest body) => await mediaControllerService.PostPhoto(body);
    
    [HttpPut("photos/{photo_id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PutPhoto([FromRoute(Name = "photo_id")]Guid photoId, [FromBody]MediaPhotoRequest body) => await mediaControllerService.PutPhoto(photoId, body);
    
    [HttpDelete("photos/{photo_id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePhoto([FromRoute(Name = "photo_id")]Guid photoId) => await mediaControllerService.DeletePhoto(photoId);
}