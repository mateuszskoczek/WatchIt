using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Movies;
using WatchIt.WebAPI.Services.Controllers.Movies;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("movies")]
public class MoviesController : ControllerBase
{
    #region SERVICES

    private readonly IMoviesControllerService _moviesControllerService;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    public MoviesController(IMoviesControllerService moviesControllerService)
    {
        _moviesControllerService = moviesControllerService;
    }
    
    #endregion
    
    
    
    #region METHODS
    
    #region Main
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<MovieResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllMovies(MovieQueryParameters query) => await _moviesControllerService.GetAllMovies(query);
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMovie([FromRoute] long id) => await _moviesControllerService.GetMovie(id);
    
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PostMovie([FromBody] MovieRequest body) => await _moviesControllerService.PostMovie(body);
    
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PutMovie([FromRoute] long id, [FromBody]MovieRequest body) => await _moviesControllerService.PutMovie(id, body);

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteMovie([FromRoute] long id) => await _moviesControllerService.DeleteMovie(id);
    
    #endregion
    
    #region View count
    
    [HttpGet("view")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<MovieResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetMoviesViewRank([FromQuery] int first = 5, [FromQuery] int days = 7) => await _moviesControllerService.GetMoviesViewRank(first, days);
    
    #endregion
    
    #endregion
}