using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.BusinessLogic.Genres;
using WatchIt.WebAPI.Constants;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("genres")]
public class GenresController(IGenresBusinessLogic genresBusinessLogic) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<GenreResponse>>> GetGenres([FromQuery] GenreFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery) =>
        await genresBusinessLogic.GetGenres(filterQuery, orderQuery, pagingQuery);

    [HttpGet("{id}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<GenreResponse>> GetGenre([FromRoute(Name = "id")] short id) =>
        await genresBusinessLogic.GetGenre(id);

    [HttpPost]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<GenreResponse>> PostGenre([FromBody] GenreRequest body) =>
        await genresBusinessLogic.PostGenre(body);

    [HttpDelete("{id}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteGenre([FromRoute(Name = "id")] short id) =>
        await genresBusinessLogic.DeleteGenre(id);
}