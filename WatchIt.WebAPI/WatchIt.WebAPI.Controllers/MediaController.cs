using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Photos;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Model.Roles;
using WatchIt.WebAPI.Services.Controllers.Media;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("media")]
public class MediaController : ControllerBase
{
    #region FIELDS
    
    private readonly IMediaControllerService _mediaControllerService;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    public MediaController(IMediaControllerService mediaControllerService)
    {
        _mediaControllerService = mediaControllerService;
    }
    
    #endregion
    
    
    
    #region METHODS
    
    #region Main
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<MediaResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllMedia(MediaQueryParameters query) => await _mediaControllerService.GetAllMedia(query);
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMedia([FromRoute] long id) => await _mediaControllerService.GetMedia(id);
    
    #endregion
    
    #region Genres
    
    [HttpGet("{id}/genres")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<GenreResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaGenres([FromRoute]long id) => await _mediaControllerService.GetMediaGenres(id);
    
    [HttpPost("{id}/genres/{genre_id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostMediaGenre([FromRoute]long id, [FromRoute(Name = "genre_id")]short genreId) => await _mediaControllerService.PostMediaGenre(id, genreId);
    
    [HttpDelete("{id}/genres/{genre_id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteMediaGenre([FromRoute]long id, [FromRoute(Name = "genre_id")]short genreId) => await _mediaControllerService.DeleteMediaGenre(id, genreId);
    
    #endregion

    #region Rating

    [HttpGet("{id}/rating")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RatingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaRating([FromRoute] long id) => await _mediaControllerService.GetMediaRating(id);
    
    [HttpGet("{id}/rating/{user_id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(short), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaRatingByUser([FromRoute] long id, [FromRoute(Name = "user_id")]long userId) => await _mediaControllerService.GetMediaRatingByUser(id, userId);
    
    [HttpPut("{id}/rating")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PutMediaRating([FromRoute] long id, [FromBody] RatingRequest data) => await _mediaControllerService.PutMediaRating(id, data);
    
    [HttpDelete("{id}/rating")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteMediaRating([FromRoute] long id) => await _mediaControllerService.DeleteMediaRating(id);
    
    #endregion

    #region View count

    [HttpPost("{id}/view")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostMediaView([FromRoute] long id) => await _mediaControllerService.PostMediaView(id);

    #endregion
    
    #region Poster
    
    [HttpGet("{id}/poster")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaPosterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaPoster([FromRoute] long id) => await _mediaControllerService.GetMediaPoster(id);
    
    [HttpPut("{id}/poster")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(MediaPosterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PutMediaPoster([FromRoute]long id, [FromBody]MediaPosterRequest body) => await _mediaControllerService.PutMediaPoster(id, body);
    
    [HttpDelete("{id}/poster")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteMediaPoster([FromRoute]long id) => await _mediaControllerService.DeleteMediaPoster(id);
    
    #endregion
    
    #region Photos
    
    [HttpGet("{id}/photos")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<PhotoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaPhotos([FromRoute]long id, PhotoQueryParameters query) => await _mediaControllerService.GetMediaPhotos(id, query);
    
    [HttpGet("{id}/photos/random_background")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaPhotoRandomBackground([FromRoute]long id) => await _mediaControllerService.GetMediaPhotoRandomBackground(id);
    
    [HttpPost("{id}/photos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(PhotoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostPhoto([FromRoute]long id, [FromBody]MediaPhotoRequest body) => await _mediaControllerService.PostMediaPhoto(id, body);
    
    #endregion

    #region Roles

    [HttpGet("{id}/roles/actor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ActorRoleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaAllActorRoles([FromRoute]long id, ActorRoleMediaQueryParameters query) => await _mediaControllerService.GetMediaAllActorRoles(id, query);
    
    [HttpPost("{id}/roles/actor")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(ActorRoleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostMediaActorRole([FromRoute]long id, [FromBody]IActorRoleMediaRequest body) => await _mediaControllerService.PostMediaActorRole(id, body);
    
    [HttpGet("{id}/roles/creator")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<CreatorRoleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaAllCreatorRoles([FromRoute]long id, CreatorRoleMediaQueryParameters query) => await _mediaControllerService.GetMediaAllCreatorRoles(id, query);
    
    [HttpPost("{id}/roles/creator")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CreatorRoleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostMediaCreatorRole([FromRoute]long id, [FromBody]ICreatorRoleMediaRequest body) => await _mediaControllerService.PostMediaCreatorRole(id, body);

    #endregion
    
    #endregion
}