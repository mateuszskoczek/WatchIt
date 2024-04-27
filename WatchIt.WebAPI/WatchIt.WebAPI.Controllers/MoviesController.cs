using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Movies;
using WatchIt.WebAPI.Services.Controllers.Movies;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("movies")]
public class MoviesController(IMoviesControllerService moviesControllerService) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<MovieResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll(MovieQueryParameters query) => await moviesControllerService.GetAll(query);
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get([FromRoute]long id) => await moviesControllerService.Get(id);
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Post([FromBody]MovieRequest body) => await moviesControllerService.Post(body);
    
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Put([FromRoute]long id, [FromBody]MovieRequest body) => await moviesControllerService.Put(id, body);
    
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Delete([FromRoute]long id) => await moviesControllerService.Delete(id);
    
    [HttpGet("{id}/genres")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<GenreResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetGenres([FromRoute]long id) => await moviesControllerService.GetGenres(id);
    
    [HttpPost("{id}/genres/{genre_id}")]
    [Authorize]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostGenre([FromRoute]long id, [FromRoute(Name = "genre_id")]short genreId) => await moviesControllerService.PostGenre(id, genreId);
    
    [HttpDelete("{id}/genres/{genre_id}")]
    [Authorize]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteGenre([FromRoute]long id, [FromRoute(Name = "genre_id")]short genreId) => await moviesControllerService.DeleteGenre(id, genreId);
}