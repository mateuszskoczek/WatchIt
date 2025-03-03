using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.Genders.Gender;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.BusinessLogic.Genders;
using WatchIt.WebAPI.Constants;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("genders")]
public class GendersController(IGendersBusinessLogic gendersBusinessLogic) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<GenderResponse>>> GetGenders([FromQuery] GenderFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery) =>
        await gendersBusinessLogic.GetGenders(filterQuery, orderQuery, pagingQuery);

    [HttpGet("{id}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<GenderResponse>> GetGender([FromRoute(Name = "id")] short id) =>
        await gendersBusinessLogic.GetGender(id);

    [HttpPost]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<GenderResponse>> PostGender([FromBody] GenderRequest body) =>
        await gendersBusinessLogic.PostGender(body);

    [HttpDelete("{id}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteGender([FromRoute(Name = "id")] short id) =>
        await gendersBusinessLogic.DeleteGender(id);
}